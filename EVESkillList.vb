
Imports System.Data.SQLite

Public Class EVESkillList

    Private Skills As List(Of EVESkill)

    Private SkillToFind As EVESkill
    Protected CheckLevelofSkilltoFind As Boolean

    Private UseActiveSkillType As Boolean

    Public Sub New(UseActiveSkill As Boolean)
        Skills = New List(Of EVESkill)
        SkillToFind = Nothing
        CheckLevelofSkilltoFind = False
        UseActiveSkillType = UseActiveSkill
    End Sub

    ' Loads all character skills into the local object
    Public Sub LoadCharacterSkills(ByVal ID As Long, ByVal CharacterTokenData As SavedTokenData,
                                   Optional ByVal LoadAllSkillsforOverride As Boolean = False,
                                   Optional SkillNameFilter As String = "")
        Dim SQL As String
        Dim rsData As SQLiteDataReader

        ' First, update the skills
        Call UpdateCharacterSkills(ID, CharacterTokenData)

        Skills = New List(Of EVESkill)

        ' Get all skills and set skill to 0 if they don't have it
        SQL = "SELECT SKILLS.SKILL_TYPE_ID,"
        SQL = SQL & "CASE WHEN CHAR_SKILLS.TRAINED_SKILL_LEVEL IS NULL THEN 0 ELSE CHAR_SKILLS.TRAINED_SKILL_LEVEL END AS TRAINED_SKILL_LEVEL,"
        SQL = SQL & "CASE WHEN CHAR_SKILLS.ACTIVE_SKILL_LEVEL IS NULL THEN 0 ELSE CHAR_SKILLS.ACTIVE_SKILL_LEVEL END AS ACTIVE_SKILL_LEVEL,"
        SQL = SQL & "CASE WHEN CHAR_SKILLS.SKILL_POINTS IS NULL THEN 0 ELSE CHAR_SKILLS.SKILL_POINTS END AS SKILL_POINTS,"
        SQL = SQL & "CASE WHEN CHAR_SKILLS.OVERRIDE_SKILL IS NULL THEN 0 ELSE CHAR_SKILLS.OVERRIDE_SKILL END AS OVERRIDE_SKILL,"
        SQL = SQL & "CASE WHEN CHAR_SKILLS.OVERRIDE_LEVEL IS NULL THEN 0 ELSE CHAR_SKILLS.OVERRIDE_LEVEL END AS OVERRIDE_LEVEL "
        SQL = SQL & "FROM SKILLS LEFT OUTER JOIN "
        SQL = SQL & "(SELECT * FROM CHARACTER_SKILLS WHERE CHARACTER_SKILLS.CHARACTER_ID=" & ID & ") AS CHAR_SKILLS "
        SQL = SQL & "ON (SKILLS.SKILL_TYPE_ID = CHAR_SKILLS.SKILL_TYPE_ID) "
        If SkillNameFilter <> "" Then
            SQL = SQL & " WHERE SKILLS.SKILL_TYPE_ID IN (SELECT SKILL_TYPE_ID FROM SKILLS WHERE SKILL_NAME LIKE '%" & FormatDBString(SkillNameFilter) & "%') "
        End If
        SQL = SQL & "ORDER BY SKILLS.SKILL_GROUP, SKILLS.SKILL_NAME "

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        rsData = DBCommand.ExecuteReader

        Dim SelectedSkillLevel As Integer = 0

        While rsData.Read
            SelectedSkillLevel = 0

            If UseActiveSkillType Then
                SelectedSkillLevel = rsData.GetInt32(2)
            Else
                SelectedSkillLevel = rsData.GetInt32(1)
            End If

            ' Insert skill
            If UserApplicationSettings.AllowSkillOverride And CBool(rsData.GetInt32(3)) And LoadAllSkillsforOverride Then
                ' Use the override skill if set, save the old skill level in the override so we can reference it later if needed
                InsertSkill(rsData.GetInt64(0), rsData.GetInt32(5), rsData.GetInt32(1), rsData.GetInt32(2), rsData.GetInt64(3), CBool(rsData.GetInt32(4)), SelectedSkillLevel)
            Else ' Just normal skills
                InsertSkill(rsData.GetInt64(0), SelectedSkillLevel, rsData.GetInt32(1), rsData.GetInt32(2), rsData.GetInt64(3), CBool(rsData.GetInt32(4)), rsData.GetInt32(5))
            End If

        End While

        rsData.Close()
        rsData = Nothing
        DBCommand = Nothing

    End Sub

    ' Updates the character skills from ESI
    Private Sub UpdateCharacterSkills(ByVal ID As Long, ByVal CharacterTokenData As SavedTokenData, Optional OpenTransaction As Boolean = True)
        Dim SQL As String = ""
        Dim readerCharacter As SQLiteDataReader
        Dim SkillList As String = ""
        Dim TempCharacterSkills As New EVESkillList(UserApplicationSettings.UseActiveSkillLevels)

        ' Get the skills for this character first
        Dim ESIData As New ESI
        Dim CB As New CacheBox
        Dim CacheDate As Date

        If CB.DataUpdateable(CacheDateType.Skills, ID) Then
            TempCharacterSkills = ESIData.GetCharacterSkills(ID, CharacterTokenData, CacheDate)

            If Not IsNothing(TempCharacterSkills) Then
                ' Clean out any skills not in the temp skills, make this first. This will ignore any skills the person may have over-ridden and added
                For i = 0 To TempCharacterSkills.GetSkillList.Count - 1
                    SkillList = SkillList & TempCharacterSkills.GetSkillList(i).TypeID & ","
                Next

                ' Strip comma
                SkillList = SkillList.Substring(0, Len(SkillList) - 1)

                ' Delete the temp skills but not any that are overridden
                SQL = "DELETE FROM CHARACTER_SKILLS WHERE SKILL_TYPE_ID IN (" & SkillList & ") AND CHARACTER_ID =" & CStr(ID)
                SQL = SQL & " AND OVERRIDE_SKILL <> -1"
                Call EVEDB.ExecuteNonQuerySQL(SQL)

                If OpenTransaction Then
                    Call EVEDB.BeginSQLiteTransaction()
                End If

                ' Insert new skill data
                For i = 0 To TempCharacterSkills.GetSkillList.Count - 1

                    ' Check for skill and update if there
                    SQL = "SELECT 'X' FROM CHARACTER_SKILLS WHERE SKILL_TYPE_ID = " & TempCharacterSkills.GetSkillList(i).TypeID & " AND CHARACTER_ID =" & CStr(ID)

                    DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                    readerCharacter = DBCommand.ExecuteReader

                    If Not readerCharacter.HasRows Then
                        ' Insert skill data
                        SQL = "INSERT INTO CHARACTER_SKILLS (CHARACTER_ID, SKILL_TYPE_ID, SKILL_NAME, SKILL_POINTS, TRAINED_SKILL_LEVEL, ACTIVE_SKILL_LEVEL, OVERRIDE_SKILL, OVERRIDE_LEVEL) "
                        SQL &= " VALUES (" & ID & "," & TempCharacterSkills.GetSkillList(i).TypeID & ",'" & TempCharacterSkills.GetSkillList(i).Name & "',"
                        SQL &= TempCharacterSkills.GetSkillList(i).SkillPoints & "," & TempCharacterSkills.GetSkillList(i).TrainedLevel & "," & TempCharacterSkills.GetSkillList(i).ActiveLevel & ",0,0)"
                    Else
                        ' Update skill data
                        SQL = "UPDATE CHARACTER_SKILLS SET "
                        SQL &= "SKILL_TYPE_ID = " & TempCharacterSkills.GetSkillList(i).TypeID & ", SKILL_NAME = '" & TempCharacterSkills.GetSkillList(i).Name & "',"
                        SQL &= "SKILL_POINTS = " & TempCharacterSkills.GetSkillList(i).SkillPoints & ", TRAINED_SKILL_LEVEL = " & TempCharacterSkills.GetSkillList(i).TrainedLevel & ", "
                        SQL &= "ACTIVE_SKILL_LEVEL = " & TempCharacterSkills.GetSkillList(i).ActiveLevel & " "
                        SQL &= "WHERE CHARACTER_ID = " & ID & " AND SKILL_TYPE_ID = " & TempCharacterSkills.GetSkillList(i).TypeID
                    End If

                    readerCharacter.Close()
                    readerCharacter = Nothing

                    Call EVEDB.ExecuteNonQuerySQL(SQL)

                Next

                DBCommand = Nothing

                ' Update cache date now all entered
                Call CB.UpdateCacheDate(CacheDateType.Skills, CacheDate, ID)

                If OpenTransaction Then
                    Call EVEDB.CommitSQLiteTransaction()
                End If

            End If
        End If

    End Sub

    ' Sets the flag for using active skills
    Public Sub SetActiveSkillFlagValue(FlagValue As Boolean)
        UseActiveSkillType = FlagValue
    End Sub

    ' Returns the skill type id for the name of the skill sent
    Public Function GetSkillTypeID(ByVal SkillName As String) As Long
        Dim i = 0

        If Not IsNothing(Skills) Then
            For i = 0 To Skills.Count - 1
                If Skills(i).Name = SkillName Then
                    Return Skills(i).TypeID
                    Exit Function
                End If
            Next
        End If

        Return 0

    End Function

    ' Returns the skill for sent name
    Public Function GetSkill(ByVal SkillName As String) As EVESkill
        Dim i = 0

        If Not IsNothing(Skills) Then
            For i = 0 To Skills.Count - 1
                If Skills(i).Name = SkillName Then
                    Return Skills(i)
                    Exit Function
                End If
            Next
        End If

        Return Nothing

    End Function

    ' Returns the level of a skill when sent the typeid of the skill
    Public Function GetSkillLevel(ByVal SkillTypeID As Long) As Integer

        ' Find the Industry Skill level
        If Not IsNothing(Skills) Then
            For i = 0 To Skills.Count - 1
                If Skills(i).TypeID = SkillTypeID Then
                    If Skills(i).Overridden Then
                        Return Skills(i).OverriddenLevel
                    Else
                        Return Skills(i).Level
                    End If
                End If
            Next
        End If

        ' Got this far we didn't find it in the list
        Return 0

    End Function

    ' Returns the level of a skill when sent the typeid of the skill
    Public Function GetSkillLevel(ByVal SkillName As String) As Integer

        ' Find the Industry Skill level
        If Not IsNothing(Skills) Then
            For i = 0 To Skills.Count - 1
                If Skills(i).Name = SkillName Then
                    If Skills(i).Overridden Then
                        Return Skills(i).OverriddenLevel
                    Else
                        Return Skills(i).Level
                    End If
                End If
            Next
        End If

        ' Got this far we didn't find it in the list
        Return 0

    End Function

    ' Returns the total skill points of a skill when sent the typeid of the skill
    Public Function GetSkillPoints(ByVal SkillTypeID As Long) As Long

        ' Find the Industry Skill level
        If Not IsNothing(Skills) Then
            For i = 0 To Skills.Count - 1
                If Skills(i).TypeID = SkillTypeID Then
                    Return Skills(i).SkillPoints
                    Exit Function
                End If
            Next
        End If

        ' Got this far we didn't find it in the list
        Return 0

    End Function

    ' Returns the list of skills for the class
    Public Function GetSkillList() As List(Of EVESkill)
        Return Skills
    End Function

    ' Returns the count of skills
    Public Function NumSkills() As Integer
        Return Skills.Count
    End Function

    ' Internal Function to insert a skill
    Private Sub InsertSkilltoList(ByVal SentSkill As EVESkill, ByVal LoadPreReqs As Boolean)
        Dim readerSkills As SQLiteDataReader
        Dim FoundSkill As New EVESkill

        Dim SQL As String

        ' See if the skill exists already
        SkillToFind = SentSkill
        CheckLevelofSkilltoFind = True
        FoundSkill = Skills.Find(AddressOf FindSkill)

        If FoundSkill IsNot Nothing Then
            ' Already here or as a level greater than this skill exists
            Exit Sub
        Else
            ' No skill or need to update the skill level
            If SentSkill.Name = "" Then
                ' Look up skill name
                SQL = "SELECT typeName, groupName FROM INVENTORY_TYPES, INVENTORY_GROUPS "
                SQL = SQL & "WHERE INVENTORY_TYPES.groupID = INVENTORY_GROUPS.groupID AND typeID = " & SentSkill.TypeID

                DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                readerSkills = DBCommand.ExecuteReader()

                If readerSkills.Read Then
                    SentSkill.Name = readerSkills.GetString(0)
                    SentSkill.Group = readerSkills.GetString(1)
                Else
                    SentSkill.Name = "Unknown Skill"
                    SentSkill.Group = "Unknown Group"
                End If
                readerSkills.Close()

            End If

            ' Check Pre-Reqs
            If LoadPreReqs Then
                SentSkill.SetPreReqSkills()
            End If

            ' Load Skill
            Skills.Add(SentSkill)

        End If

    End Sub

    ' Allows inserting a skill by structure
    Public Sub InsertSkill(ByVal InsertSkill As EVESkill, Optional ByVal LoadPreReqs As Boolean = False)
        ' Set the Level based on skill type chosen
        If UseActiveSkillType Then
            InsertSkill.Level = InsertSkill.ActiveLevel
        Else
            InsertSkill.Level = InsertSkill.TrainedLevel
        End If
        Call InsertSkilltoList(InsertSkill, LoadPreReqs)
    End Sub

    ' Inserts a skill into the list
    Public Sub InsertSkill(ByVal SkillTypeID As Long, ByVal Level As Integer, ByVal TrainedSkillLevel As Integer, ActiveSkillLevel As Integer, ByVal SkillPoints As Long,
                           ByVal SkillOverriden As Boolean, ByVal SkillOverrideLevel As Integer,
                           Optional ByVal SkillName As String = "", Optional ByVal PreReqSkills As EVESkillList = Nothing,
                           Optional ByVal LoadPreReqs As Boolean = False)

        Dim InsertSkill As New EVESkill

        InsertSkill.TypeID = SkillTypeID
        InsertSkill.Level = Level
        InsertSkill.TrainedLevel = TrainedSkillLevel
        InsertSkill.ActiveLevel = ActiveSkillLevel
        InsertSkill.Name = SkillName
        InsertSkill.SkillPoints = SkillPoints
        InsertSkill.Overridden = SkillOverriden
        InsertSkill.OverriddenLevel = SkillOverrideLevel

        If Not IsNothing(PreReqSkills) Then
            InsertSkill.PreReqSkills = PreReqSkills
        End If

        Call InsertSkilltoList(InsertSkill, LoadPreReqs)

    End Sub

    ' Inserts a set of character skills into the current set 
    Public Sub InsertSkills(ByVal TempSkills As EVESkillList, ByVal LoadPreReqSkills As Boolean)
        Dim i As Integer

        If Not IsNothing(TempSkills) Then
            For i = 0 To TempSkills.NumSkills - 1
                With TempSkills
                    InsertSkill(TempSkills.GetSkillList(i), LoadPreReqSkills)
                End With
            Next
        End If

    End Sub

    ' Will update the skill level for the skill name sent in the list - If it doesn't exist, it won't update anything
    Public Sub UpdateSkill(ByVal SkillUpdate As EVESkill)
        Dim i As Integer

        If Not IsNothing(Skills) Then
            For i = 0 To Skills.Count - 1
                If Skills(i).Name = SkillUpdate.Name Then
                    ' Update the skill
                    Skills(i) = SkillUpdate
                End If
            Next
        End If

    End Sub

    ' Predicate for finding the skill
    Private Function FindSkill(ByVal SSkill As EVESkill) As Boolean

        If SSkill.TypeID = SkillToFind.TypeID Then
            If CheckLevelofSkilltoFind And SSkill.Level <= SkillToFind.Level Then
                Return True
            ElseIf Not CheckLevelofSkilltoFind Then
                Return True
            Else
                Return False
            End If
        Else
            Return False
        End If

    End Function

    ' Save the overridden skills
    Public Sub SaveOverRideSkills(ByVal OverRideSkills As EVESkillList)
        Dim i As Integer
        Dim readerSkills As SQLiteDataReader
        Dim SQL As String

        ' Loop through all the override skills and update as necessary, then set the character skills to these
        If OverRideSkills.NumSkills <> 0 Then

            ' Update their user id to override skills
            SQL = "UPDATE ESI_CHARACTER_DATA SET OVERRIDE_SKILLS = 1 WHERE CHARACTER_ID = " & SelectedCharacter.ID
            Call EVEDB.ExecuteNonQuerySQL(SQL)

            Call EVEDB.BeginSQLiteTransaction()

            For i = 0 To OverRideSkills.NumSkills - 1
                ' Two possiblities - the skill exists, which is where we update the override variables, it doesn't and we enter a new record
                ' or it's there but we want to remove it
                ' Check for skill and update if there
                SQL = "SELECT SKILL_LEVEL FROM CHARACTER_SKILLS WHERE SKILL_TYPE_ID = " & OverRideSkills.Skills(i).TypeID & " AND CHARACTER_ID =" & SelectedCharacter.ID

                DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
                readerSkills = DBCommand.ExecuteReader

                If readerSkills.Read Then
                    If CInt(readerSkills.GetValue(0)) = 0 And Not OverRideSkills.Skills(i).Level = 0 Then
                        ' This user doesn't want to save this skill and we need to delete the old one
                        SQL = "DELETE FROM CHARACTER_SKILLS WHERE SKILL_TYPE_ID = " & OverRideSkills.Skills(i).TypeID & " AND CHARACTER_ID =" & SelectedCharacter.ID
                    Else ' It's here and we need to update it
                        SQL = "UPDATE CHARACTER_SKILLS SET "
                        SQL = SQL & "OVERRIDE_SKILL = " & CInt(OverRideSkills.Skills(i).Overridden) & ", OVERRIDE_LEVEL = " & OverRideSkills.Skills(i).OverriddenLevel & " "
                        SQL = SQL & "WHERE SKILL_TYPE_ID = " & OverRideSkills.Skills(i).TypeID & " AND CHARACTER_ID =" & SelectedCharacter.ID
                    End If
                Else
                    ' Insert the skill but since the user didn't have this, set the skill level to 0
                    SQL = "INSERT INTO CHARACTER_SKILLS (CHARACTER_ID, SKILL_TYPE_ID, SKILL_NAME, SKILL_POINTS, TRAINED_SKILL_LEVEL, ACTIVE_SKILL_LEVEL, OVERRIDE_SKILL, OVERRIDE_LEVEL) "
                    SQL = SQL & " VALUES (" & SelectedCharacter.ID & "," & OverRideSkills.Skills(i).TypeID & ",'" & OverRideSkills.Skills(i).Name & "',"
                    SQL = SQL & OverRideSkills.Skills(i).SkillPoints & ",0,0," & CInt(OverRideSkills.Skills(i).Overridden) & "," & OverRideSkills.Skills(i).OverriddenLevel & ")"
                End If

                readerSkills.Close()

                Call EVEDB.ExecuteNonQuerySQL(SQL)

                UserApplicationSettings.AllowSkillOverride = True

            Next

            Call EVEDB.CommitSQLiteTransaction()

            ' Just saved the skill updates so, only reload the skills from db
            Call SelectedCharacter.Skills.LoadCharacterSkills(SelectedCharacter.ID, SelectedCharacter.CharacterTokenData, False)

        Else
            ' Clean up the skills because we are reverting to default API skills
            ' Change their override user setting back to unchecked
            SQL = "UPDATE ESI_CHARACTER_DATA SET OVERRIDE_SKILLS = 0 WHERE CHARACTER_ID = " & SelectedCharacter.ID
            Call EVEDB.ExecuteNonQuerySQL(SQL)

            ' Delete any skills that aren't trained by the character
            SQL = "DELETE FROM CHARACTER_SKILLS WHERE CHARACTER_ID = " & SelectedCharacter.ID & " AND TRAINED_SKILL_LEVEL = 0"
            Call EVEDB.ExecuteNonQuerySQL(SQL)

            ' Update all skills to base
            SQL = "UPDATE CHARACTER_SKILLS SET OVERRIDE_LEVEL = 0, OVERRIDE_SKILL = 0 WHERE CHARACTER_ID = " & SelectedCharacter.ID
            Call EVEDB.ExecuteNonQuerySQL(SQL)

            UserApplicationSettings.AllowSkillOverride = False

        End If

    End Sub

    ' Loads the skills for a 'Dummy Character' to this character
    Public Sub LoadDummySkills()
        Dim SQL As String
        Dim DummySkills As New EVESkillList(UserApplicationSettings.UseActiveSkillLevels)

        ' Add skills for a brand new newbie char 
        DummySkills.InsertSkill(3300, 2, 2, 2, 1415, False, 0, "Gunnery")
        DummySkills.InsertSkill(3301, 3, 3, 3, 8000, False, 0, "Small Hybrid Turret")
        DummySkills.InsertSkill(3302, 3, 3, 3, 8000, False, 0, "Small Projectile Turret")
        DummySkills.InsertSkill(3303, 3, 3, 3, 8000, False, 0, "Small Energy Turret")
        DummySkills.InsertSkill(3327, 3, 3, 3, 8000, False, 0, "Spaceship Command")
        DummySkills.InsertSkill(3328, 2, 2, 2, 2829, False, 0, "Gallente Frigate")
        DummySkills.InsertSkill(3329, 2, 2, 2, 2829, False, 0, "Minmatar Frigate")
        DummySkills.InsertSkill(3330, 2, 2, 2, 2829, False, 0, "Caldari Frigate")
        DummySkills.InsertSkill(3381, 2, 2, 2, 2829, False, 0, "Amarr Frigate")
        DummySkills.InsertSkill(3386, 2, 2, 2, 1415, False, 0, "Mining")
        DummySkills.InsertSkill(3402, 3, 3, 3, 8000, False, 0, "Science")
        DummySkills.InsertSkill(3392, 3, 3, 3, 8000, False, 0, "Mechanics")
        DummySkills.InsertSkill(3413, 3, 3, 3, 8000, False, 0, "Engineering")
        DummySkills.InsertSkill(3426, 3, 3, 3, 8000, False, 0, "Electronics")
        DummySkills.InsertSkill(3449, 3, 3, 3, 8000, False, 0, "Navigation")

        ' Just save the current list as the main skills
        Skills = DummySkills.GetSkillList

        ' Clean up any skills if they exist - account does not so load a fresh set
        SQL = "DELETE FROM CHARACTER_SKILLS WHERE CHARACTER_ID = " & CStr(DummyCharacterID)
        Call EVEDB.ExecuteNonQuerySQL(SQL)

        ' Insert skill records for dummy
        SQL = "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3426,'Electronics',8000,3,0,0,0)"
        Call EVEDB.ExecuteNonQuerySQL(SQL)
        SQL = "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3413,'Engineering',8000,3,0,0,0)"
        Call EVEDB.ExecuteNonQuerySQL(SQL)
        SQL = "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3386,'Mining',1415,2,0,0,0)"
        Call EVEDB.ExecuteNonQuerySQL(SQL)
        SQL = "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3392,'Mechanics',8000,3,0,0,0)"
        Call EVEDB.ExecuteNonQuerySQL(SQL)
        SQL = "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3449,'Navigation',8000,3,0,0,0)"
        Call EVEDB.ExecuteNonQuerySQL(SQL)
        SQL = "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3402,'Science',8000,3,0,0,0)"
        Call EVEDB.ExecuteNonQuerySQL(SQL)
        SQL = "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3327,'Spaceship Command',8000,3,0,0,0)"
        Call EVEDB.ExecuteNonQuerySQL(SQL)
        SQL = "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3381,'Amarr Frigate',2829,2,0,0,0)"
        Call EVEDB.ExecuteNonQuerySQL(SQL)
        SQL = "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3330,'Caldari Frigate',2829,2,0,0,0)"
        Call EVEDB.ExecuteNonQuerySQL(SQL)
        SQL = "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3328,'Gallente Frigate',2829,2,0,0,0)"
        Call EVEDB.ExecuteNonQuerySQL(SQL)
        SQL = "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3329,'Minmatar Frigate',2829,2,0,0,0)"
        Call EVEDB.ExecuteNonQuerySQL(SQL)
        SQL = "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3300,'Gunnery',1415,2,0,0,0)"
        Call EVEDB.ExecuteNonQuerySQL(SQL)
        SQL = "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3303,'Small Energy Turret',8000,3,0,0,0)"
        Call EVEDB.ExecuteNonQuerySQL(SQL)
        SQL = "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3301,'Small Hybrid Turret',8000,3,0,0,0)"
        Call EVEDB.ExecuteNonQuerySQL(SQL)
        SQL = "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3302,'Small Projectile Turret',8000,3,0,0,0)"
        Call EVEDB.ExecuteNonQuerySQL(SQL)

    End Sub

End Class

Public Class EVESkill
    Public TypeID As Long
    Public Name As String
    Public Group As String
    Public SkillPoints As Long
    Public TrainedLevel As Integer
    Public ActiveLevel As Integer
    Public Level As Integer ' What we use in IPH
    Public Overridden As Boolean
    Public OverriddenLevel As Integer
    Public PreReqSkills As EVESkillList

    Public Sub New()
        TypeID = 0
        Name = ""
        Group = ""
        SkillPoints = 0
        TrainedLevel = 0
        ActiveLevel = 0
        Level = 0
        Overridden = False
        OverriddenLevel = 0
        PreReqSkills = New EVESkillList(UserApplicationSettings.UseActiveSkillLevels)
    End Sub

    Public Sub SetPreReqSkills()
        Dim SQL As String
        Dim PreReqs As New EVESkillList(UserApplicationSettings.UseActiveSkillLevels)
        Dim TempSkill As New EVESkill
        Dim readerSkills As SQLiteDataReader

        SQL = "SELECT CASE WHEN TYPE_ATTRIBUTES.valueInt IS NULL THEN TYPE_ATTRIBUTES.valueFloat ELSE TYPE_ATTRIBUTES.valueInt END, "
        SQL = SQL & "INVENTORY_TYPES_1.typeName, INVENTORY_GROUPS.groupName, "
        SQL = SQL & "CASE WHEN TYPE_ATTRIBUTES_1.valueInt IS NULL THEN TYPE_ATTRIBUTES_1.valueFloat ELSE TYPE_ATTRIBUTES_1.valueInt END "
        SQL = SQL & "FROM INVENTORY_TYPES, INVENTORY_GROUPS, INVENTORY_TYPES AS INVENTORY_TYPES_1, TYPE_ATTRIBUTES, TYPE_ATTRIBUTES AS TYPE_ATTRIBUTES_1, ATTRIBUTE_TYPES "
        SQL = SQL & "WHERE INVENTORY_TYPES.typeID = TYPE_ATTRIBUTES.typeID "
        SQL = SQL & "AND INVENTORY_TYPES.groupID = INVENTORY_GROUPS.groupID "
        SQL = SQL & "AND TYPE_ATTRIBUTES.attributeID = ATTRIBUTE_TYPES.attributeID "
        SQL = SQL & "AND INVENTORY_TYPES.typeID = TYPE_ATTRIBUTES_1.typeID "
        SQL = SQL & "AND CASE WHEN TYPE_ATTRIBUTES.valueInt IS NULL THEN TYPE_ATTRIBUTES.valueFloat ELSE TYPE_ATTRIBUTES.valueInt END = INVENTORY_TYPES_1.typeID "
        SQL = SQL & "AND TYPE_ATTRIBUTES.attributeID > 181 AND TYPE_ATTRIBUTES.attributeID < 185 AND TYPE_ATTRIBUTES_1.attributeID = TYPE_ATTRIBUTES.attributeID + 95 "
        SQL = SQL & "AND INVENTORY_TYPES.typeID = " & CStr(TypeID)

        DBCommand = New SQLiteCommand(SQL, EVEDB.DBREf)
        readerSkills = DBCommand.ExecuteReader

        While readerSkills.Read
            With readerSkills
                TempSkill.TypeID = CInt(.GetDouble(0))
                TempSkill.Name = .GetString(1)
                TempSkill.Group = .GetString(2)
                TempSkill.TrainedLevel = CInt(.GetValue(3))
                TempSkill.ActiveLevel = CInt(.GetValue(3))
                TempSkill.Level = CInt(.GetValue(3))
                TempSkill.SkillPoints = 0
                TempSkill.OverriddenLevel = 0
                TempSkill.Overridden = False
                TempSkill.SetPreReqSkills()
            End With

            ' Set the local pre-reqs
            With TempSkill
                PreReqs.InsertSkill(.TypeID, .Level, .TrainedLevel, .ActiveLevel, .SkillPoints, .Overridden, .OverriddenLevel, .Name, .PreReqSkills, True)
            End With
        End While

        ' Save the final set
        PreReqSkills = PreReqs

        readerSkills.Close()
        readerSkills = Nothing
        DBCommand = Nothing

    End Sub

End Class