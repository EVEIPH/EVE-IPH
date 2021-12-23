
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
        If ID <> DummyCharacterID Then
            Call UpdateCharacterSkills(ID, CharacterTokenData)
        End If

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
            If UserApplicationSettings.AllowSkillOverride And CBool(rsData.GetInt32(4)) And LoadAllSkillsforOverride Then
                ' Use the override skill if set, save the old skill level in the override so we can reference it later if needed
                InsertSkill(rsData.GetInt64(0), rsData.GetInt32(5), rsData.GetInt32(1), rsData.GetInt32(2), rsData.GetInt64(3), CBool(rsData.GetInt32(4)), SelectedSkillLevel)
            Else ' Just normal skills
                InsertSkill(rsData.GetInt64(0), SelectedSkillLevel, rsData.GetInt32(1), rsData.GetInt32(2), rsData.GetInt64(3), CBool(rsData.GetInt32(4)), rsData.GetInt32(5))
            End If

        End While

        rsData.Close()

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

            Call EVEDB.BeginSQLiteTransaction()

            ' Update their user id to override skills
            SQL = "UPDATE ESI_CHARACTER_DATA SET OVERRIDE_SKILLS = 1 WHERE CHARACTER_ID = " & SelectedCharacter.ID
            Call EVEDB.ExecuteNonQuerySQL(SQL)

            For i = 0 To OverRideSkills.NumSkills - 1
                ' Two possiblities - the skill exists, which is where we update the override variables, it doesn't and we enter a new record
                ' or it's there but we want to remove it
                ' Check for skill and update if there

                Dim SkillType As String
                If UserApplicationSettings.UseActiveSkillLevels Then
                    SkillType = "ACTIVE_SKILL_LEVEL"
                Else
                    SkillType = "TRAINED_SKILL_LEVEL"
                End If
                SQL = "SELECT " & SkillType & " FROM CHARACTER_SKILLS WHERE SKILL_TYPE_ID = " & OverRideSkills.Skills(i).TypeID & " AND CHARACTER_ID =" & SelectedCharacter.ID

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

        ' Clean up any skills if they exist - account does not so load a fresh set
        SQL = "DELETE FROM CHARACTER_SKILLS WHERE CHARACTER_ID = " & CStr(DummyCharacterID)
        Call EVEDB.ExecuteNonQuerySQL(SQL)

        ' Insert skill records for dummy
        SQL = "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3327,'Spaceship Command',8000,3,3,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3380,'Industry',250,1,1,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3386,'Mining',1415,2,2,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3402,'Science',8000,3,3,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",32918,'Mining Frigate',8000,2,2,0,0); "
        Call EVEDB.ExecuteNonQuerySQL(SQL)

        ' Just saved the skill updates so, only reload the skills from db
        Call SelectedCharacter.Skills.LoadCharacterSkills(SelectedCharacter.ID, SelectedCharacter.CharacterTokenData, False)

    End Sub

    ' Loads the max alpha skills for this character (option for dummy accounts)
    Public Sub LoadMaxAlphaSkills()
        Dim SQL As String

        ' Clean up any skills if they exist - account does not so load a fresh set
        SQL = "DELETE FROM CHARACTER_SKILLS WHERE CHARACTER_ID = " & CStr(DummyCharacterID)
        Call EVEDB.ExecuteNonQuerySQL(SQL)

        ' Insert skill records for max alpha
        SQL = "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",33078,'Armor Layering',750,3,3,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",22806,'EM Armor Compensation',2828,2,2,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",22807,'Explosive Armor Compensation',2828,2,2,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3394,'Hull Upgrades',512000,2,2,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",22808,'Kinetic Armor Compensation',2828,2,2,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3392,'Mechanics',256000,1,1,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",16069,'Remote Armor Repair Systems',16000,2,2,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",27902,'Remote Hull Repair Systems',2828,2,2,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3393,'Repair Systems',256000,1,1,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",22809,'Thermal Armor Compensation',2828,2,2,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3363,'Corporation Management',250,1,1,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",12484,'Amarr Drone Specialization',7071,5,5,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",12487,'Caldari Drone Specialization',7071,5,5,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3437,'Drone Avionics',45255,1,1,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",23618,'Drone Durability',226274,5,5,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3442,'Drone Interfacing',226274,5,5,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",12305,'Drone Navigation',45255,1,1,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",23606,'Drone Sharpshooting',45255,1,1,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3436,'Drones',256000,1,1,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",12486,'Gallente Drone Specialization',7071,5,5,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3441,'Heavy Drone Operation',226274,5,5,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",24241,'Light Drone Operation',256000,1,1,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",33699,'Medium Drone Operation',512000,2,2,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",12485,'Minmatar Drone Specialization',7071,5,5,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3439,'Repair Drone Operation',4243,3,3,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3427,'Electronic Warfare',90510,2,2,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3435,'Propulsion Jamming',135765,3,3,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3433,'Sensor Linking',24000,3,3,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",19921,'Target Painting',24000,3,3,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3434,'Weapon Disruption',135765,3,3,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",11207,'Advanced Weapon Upgrades',48000,6,6,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3423,'Capacitor Emission Systems',90510,2,2,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3418,'Capacitor Management',135765,3,3,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3417,'Capacitor Systems Operation',8000,1,1,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3426,'CPU Management',256000,1,1,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3432,'Electronics Upgrades',512000,2,2,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3424,'Energy Grid Upgrades',512000,2,2,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3421,'Energy Pulse Weapons',2828,2,2,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3413,'Power Grid Management',256000,1,1,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",28164,'Thermodynamics',135765,3,3,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3318,'Weapon Upgrades',512000,2,2,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3316,'Controlled Bursts',90510,2,2,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3300,'Gunnery',256000,1,1,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",25718,'Heavy Assault Missile Specialization',40000,5,5,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",20211,'Heavy Missile Specialization',40000,5,5,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3309,'Large Energy Turret',226274,5,5,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3307,'Large Hybrid Turret',226274,5,5,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3308,'Large Projectile Turret',226274,5,5,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",20210,'Light Missile Specialization',24000,3,3,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",12202,'Medium Artillery Specialization',40000,5,5,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",12208,'Medium Autocannon Specialization',40000,5,5,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",12204,'Medium Beam Laser Specialization',40000,5,5,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",12211,'Medium Blaster Specialization',40000,5,5,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3306,'Medium Energy Turret',768000,3,3,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3304,'Medium Hybrid Turret',768000,3,3,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3305,'Medium Projectile Turret',768000,3,3,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",12214,'Medium Pulse Laser Specialization',40000,5,5,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",12206,'Medium Railgun Specialization',40000,5,5,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3312,'Motion Prediction',90510,2,2,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3310,'Rapid Firing',90510,2,2,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",20209,'Rocket Specialization',24000,3,3,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3311,'Sharpshooter',90510,2,2,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",12201,'Small Artillery Specialization',24000,3,3,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",11084,'Small Autocannon Specialization',24000,3,3,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",11083,'Small Beam Laser Specialization',24000,3,3,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",12210,'Small Blaster Specialization',24000,3,3,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3303,'Small Energy Turret',256000,1,1,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3301,'Small Hybrid Turret',256000,1,1,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3302,'Small Projectile Turret',256000,1,1,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",12213,'Small Pulse Laser Specialization',24000,3,3,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",11082,'Small Railgun Specialization',24000,3,3,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3315,'Surgical Strike',181019,4,4,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3317,'Trajectory Analysis',226274,5,5,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3348,'Leadership',8000,1,1,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3326,'Cruise Missiles',226274,5,5,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",20312,'Guided Missile Precision',40000,5,5,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",25719,'Heavy Assault Missiles',768000,3,3,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3324,'Heavy Missiles',768000,3,3,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3321,'Light Missiles',512000,2,2,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",12441,'Missile Bombardment',90510,2,2,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3319,'Missile Launcher Operation',256000,1,1,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",12442,'Missile Projection',5657,4,4,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",21071,'Rapid Launch',90510,2,2,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3320,'Rockets',256000,1,1,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",20314,'Target Navigation Prediction',16000,2,2,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3325,'Torpedoes',226274,5,5,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",20315,'Warhead Upgrades',40000,5,5,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3452,'Acceleration Control',32000,4,4,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3450,'Afterburner',8000,1,1,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3453,'Evasive Maneuvering',16000,2,2,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3454,'High Speed Maneuvering',40000,5,5,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3449,'Navigation',45255,1,1,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3455,'Warp Drive Operation',8000,1,1,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3405,'Biology',8000,1,1,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3411,'Cybernetics',24000,3,3,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",24242,'Infomorph Psychology',250,1,1,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3380,'Industry',256000,1,1,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3387,'Mass Production',16000,2,2,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",25544,'Gas Cloud Harvesting',1414,1,1,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",16281,'Ice Harvesting',1414,1,1,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3386,'Mining',45255,1,1,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",22578,'Mining Upgrades',181019,4,4,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3385,'Reprocessing',8000,1,1,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",25863,'Salvaging',24000,3,3,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",26253,'Armor Rigging',24000,3,3,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",26254,'Astronautics Rigging',24000,3,3,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",26255,'Drones Rigging',24000,3,3,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",26256,'Electronic Superiority Rigging',24000,3,3,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",26258,'Energy Weapon Rigging',24000,3,3,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",26259,'Hybrid Weapon Rigging',24000,3,3,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",26252,'Jury Rigging',16000,2,2,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",26260,'Launcher Rigging',24000,3,3,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",26257,'Projectile Weapon Rigging',24000,3,3,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",26261,'Shield Rigging',24000,3,3,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",13278,'Archaeology',24000,3,3,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",25811,'Astrometric Acquisition',7071,5,5,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",25739,'Astrometric Rangefinding',11314,8,8,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3412,'Astrometrics',24000,3,3,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",21718,'Hacking',24000,3,3,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3551,'Survey',8000,1,1,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3402,'Science',45255,1,1,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",12365,'EM Shield Compensation',2828,2,2,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",12367,'Explosive Shield Compensation',2828,2,2,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",12366,'Kinetic Shield Compensation',2828,2,2,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",21059,'Shield Compensation',90510,2,2,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3422,'Shield Emission Systems',16000,2,2,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3419,'Shield Management',135765,3,3,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3416,'Shield Operation',45255,1,1,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3425,'Shield Upgrades',90510,2,2,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3420,'Tactical Shield Manipulation',181019,4,4,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",11566,'Thermal Shield Compensation',2828,2,2,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3359,'Connections',4243,3,3,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3361,'Criminal Connections',4243,3,3,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3357,'Diplomacy',8000,1,1,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3894,'Distribution Connections',2828,2,2,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3893,'Mining Connections',2828,2,2,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3356,'Negotiation',2828,2,2,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3895,'Security Connections',2828,2,2,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3355,'Social',8000,1,1,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",33095,'Amarr Battlecruiser',271529,6,6,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3339,'Amarr Battleship',362309,8,8,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3335,'Amarr Cruiser',226274,5,5,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",33091,'Amarr Destroyer',90510,2,2,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3331,'Amarr Frigate',90510,2,2,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3343,'Amarr Industrial',1000,4,4,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",33096,'Caldari Battlecruiser',271529,6,6,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3338,'Caldari Battleship',362309,8,8,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3334,'Caldari Cruiser',226274,5,5,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",33092,'Caldari Destroyer',90510,2,2,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3330,'Caldari Frigate',90510,2,2,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3342,'Caldari Industrial',1000,4,4,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",33097,'Gallente Battlecruiser',271529,6,6,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3336,'Gallente Battleship',362309,8,8,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3332,'Gallente Cruiser',226274,5,5,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",33093,'Gallente Destroyer',90510,2,2,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3328,'Gallente Frigate',90510,2,2,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3340,'Gallente Industrial',1000,4,4,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",32918,'Mining Frigate',90510,2,2,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",33098,'Minmatar Battlecruiser',271529,6,6,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3337,'Minmatar Battleship',362309,8,8,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3333,'Minmatar Cruiser',226274,5,5,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",33094,'Minmatar Destroyer',90510,2,2,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3329,'Minmatar Frigate',90510,2,2,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3341,'Minmatar Industrial',100,4,4,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3327,'Spaceship Command',8000,1,1,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",11584,'Anchoring',750,3,3,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3428,'Long Range Targeting',16000,2,2,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3431,'Signature Analysis',8000,1,1,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3429,'Target Management',45255,1,1,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3446,'Broker Relations',2828,2,2,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",16598,'Marketing',4243,3,3,0,0); "
        SQL &= "INSERT INTO CHARACTER_SKILLS VALUES (" & CStr(DummyCharacterID) & ",3443,'Trade',8000,1,1,0,0); "

        Call EVEDB.ExecuteNonQuerySQL(SQL)

        ' Just saved the skill updates so, only reload the skills from db
        Call SelectedCharacter.Skills.LoadCharacterSkills(SelectedCharacter.ID, SelectedCharacter.CharacterTokenData, False)

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

        SQL = "SELECT TYPE_ATTRIBUTES.value, INVENTORY_TYPES_1.typeName, INVENTORY_GROUPS.groupName, TYPE_ATTRIBUTES_1.value "
        SQL = SQL & "FROM INVENTORY_TYPES, INVENTORY_GROUPS, INVENTORY_TYPES AS INVENTORY_TYPES_1, TYPE_ATTRIBUTES, TYPE_ATTRIBUTES AS TYPE_ATTRIBUTES_1, ATTRIBUTE_TYPES "
        SQL = SQL & "WHERE INVENTORY_TYPES.typeID = TYPE_ATTRIBUTES.typeID "
        SQL = SQL & "AND INVENTORY_TYPES.groupID = INVENTORY_GROUPS.groupID "
        SQL = SQL & "AND TYPE_ATTRIBUTES.attributeID = ATTRIBUTE_TYPES.attributeID "
        SQL = SQL & "AND INVENTORY_TYPES.typeID = TYPE_ATTRIBUTES_1.typeID "
        SQL = SQL & "AND TYPE_ATTRIBUTES.value = INVENTORY_TYPES_1.typeID "
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

    End Sub

End Class
