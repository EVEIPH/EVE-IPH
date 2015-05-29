
Imports System.Data.SQLite

Public Class EVESkillList

    Private Skills As List(Of EVESkill)

    Private SkillToFind As EVESkill
    Protected CheckLevelofSkilltoFind As Boolean

    Public Sub New()
        Skills = New List(Of EVESkill)
        SkillToFind = Nothing
        CheckLevelofSkilltoFind = False
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
                    Return Skills(i).Level
                    Exit Function
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
                    Return Skills(i).Level
                    Exit Function
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

                DBCommand = New SQLiteCommand(SQL, DB)
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
        Call InsertSkilltoList(InsertSkill, LoadPreReqs)
    End Sub

    ' Inserts a skill into the list
    Public Sub InsertSkill(ByVal SkillTypeID As Long, ByVal SkillLevel As Integer, ByVal SkillPoints As Long, _
                           ByVal SkillOverriden As Boolean, ByVal SkillOverrideLevel As Integer, _
                           Optional ByVal SkillName As String = "", Optional ByVal PreReqSkills As EVESkillList = Nothing, _
                           Optional ByVal LoadPreReqs As Boolean = False)

        Dim InsertSkill As New EVESkill

        InsertSkill.TypeID = SkillTypeID
        InsertSkill.Level = SkillLevel
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
            SQL = "UPDATE API SET OVERRIDE_SKILLS = 1 WHERE CHARACTER_ID = " & SelectedCharacter.ID & " AND API_TYPE NOT IN ('Corporation', 'Old Key')"
            Call ExecuteNonQuerySQL(SQL)

            Call BeginSQLiteTransaction()

            For i = 0 To OverRideSkills.NumSkills - 1
                ' Two possiblities - the skill exists, which is where we update the override variables, it doesn't and we enter a new record
                ' or it's there but we want to remove it
                ' Check for skill and update if there
                SQL = "SELECT SKILL_LEVEL FROM CHARACTER_SKILLS WHERE SKILL_TYPE_ID = " & OverRideSkills.Skills(i).TypeID & " AND CHARACTER_ID =" & SelectedCharacter.ID

                DBCommand = New SQLiteCommand(SQL, DB)
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
                    SQL = "INSERT INTO CHARACTER_SKILLS (CHARACTER_ID, SKILL_TYPE_ID, SKILL_NAME, SKILL_POINTS, SKILL_LEVEL, OVERRIDE_SKILL, OVERRIDE_LEVEL) "
                    SQL = SQL & " VALUES (" & SelectedCharacter.ID & "," & OverRideSkills.Skills(i).TypeID & ",'" & OverRideSkills.Skills(i).Name & "',"
                    SQL = SQL & OverRideSkills.Skills(i).SkillPoints & ",0," & CInt(OverRideSkills.Skills(i).Overridden) & "," & OverRideSkills.Skills(i).OverriddenLevel & ")"
                End If

                readerSkills.Close()

                Call ExecuteNonQuerySQL(SQL)

                UserApplicationSettings.AllowSkillOverride = True

            Next

            Call CommitSQLiteTransaction()

            ' Just saved the skill updates so, only reload the skills from db
            SelectedCharacter.LoadSkills(False, False)

        Else
            ' Clean up the skills because we are reverting to default API skills
            ' Change their override user setting back to unchecked
            SQL = "UPDATE API SET OVERRIDE_SKILLS = 0 WHERE CHARACTER_ID = " & SelectedCharacter.ID & " AND API_TYPE NOT IN ('Corporation', 'Old Key')"
            Call ExecuteNonQuerySQL(SQL)

            ' Delete any skills that aren't trained by the character
            SQL = "DELETE FROM CHARACTER_SKILLS WHERE CHARACTER_ID = " & SelectedCharacter.ID & " AND SKILL_LEVEL = 0"
            Call ExecuteNonQuerySQL(SQL)

            ' Update all skills to base
            SQL = "UPDATE CHARACTER_SKILLS SET OVERRIDE_LEVEL = 0, OVERRIDE_SKILL = 0 WHERE CHARACTER_ID = " & SelectedCharacter.ID
            Call ExecuteNonQuerySQL(SQL)

            UserApplicationSettings.AllowSkillOverride = False

        End If

    End Sub

    ' Loads the skills for a 'Dummy Character' to this character
    Public Sub LoadDummySkills()
        Dim SQL As String
        Dim DummySkills As New EVESkillList

        ' Add skills for a brand new newbie char 
        DummySkills.InsertSkill(3300, 2, 1415, False, 0, "Gunnery")
        DummySkills.InsertSkill(3301, 3, 8000, False, 0, "Small Hybrid Turret")
        DummySkills.InsertSkill(3302, 3, 8000, False, 0, "Small Projectile Turret")
        DummySkills.InsertSkill(3303, 3, 8000, False, 0, "Small Energy Turret")
        DummySkills.InsertSkill(3327, 3, 8000, False, 0, "Spaceship Command")
        DummySkills.InsertSkill(3328, 2, 2829, False, 0, "Gallente Frigate")
        DummySkills.InsertSkill(3329, 2, 2829, False, 0, "Minmatar Frigate")
        DummySkills.InsertSkill(3330, 2, 2829, False, 0, "Caldari Frigate")
        DummySkills.InsertSkill(3381, 2, 2829, False, 0, "Amarr Frigate")
        DummySkills.InsertSkill(3386, 2, 1415, False, 0, "Mining")
        DummySkills.InsertSkill(3402, 3, 8000, False, 0, "Science")
        DummySkills.InsertSkill(3392, 3, 8000, False, 0, "Mechanics")
        DummySkills.InsertSkill(3413, 3, 8000, False, 0, "Engineering")
        DummySkills.InsertSkill(3426, 3, 8000, False, 0, "Electronics")
        DummySkills.InsertSkill(3449, 3, 8000, False, 0, "Navigation")

        ' Just save the current list as the main skills
        Skills = DummySkills.GetSkillList

        ' Clean up any skills if they exist - account does not so load a fresh set
        Sql = "DELETE FROM CHARACTER_SKILLS WHERE CHARACTER_ID = 0"
        Call ExecuteNonQuerySQL(Sql)

        ' Insert skill records for dummy
        Sql = "INSERT INTO CHARACTER_SKILLS VALUES (0,3426,'Electronics',8000,3,0,0)"
        Call ExecuteNonQuerySQL(Sql)
        Sql = "INSERT INTO CHARACTER_SKILLS VALUES (0,3413,'Engineering',8000,3,0,0)"
        Call ExecuteNonQuerySQL(Sql)
        Sql = "INSERT INTO CHARACTER_SKILLS VALUES (0,3386,'Mining',1415,2,0,0)"
        Call ExecuteNonQuerySQL(Sql)
        Sql = "INSERT INTO CHARACTER_SKILLS VALUES (0,3392,'Mechanics',8000,3,0,0)"
        Call ExecuteNonQuerySQL(Sql)
        Sql = "INSERT INTO CHARACTER_SKILLS VALUES (0,3449,'Navigation',8000,3,0,0)"
        Call ExecuteNonQuerySQL(Sql)
        Sql = "INSERT INTO CHARACTER_SKILLS VALUES (0,3402,'Science',8000,3,0,0)"
        Call ExecuteNonQuerySQL(Sql)
        Sql = "INSERT INTO CHARACTER_SKILLS VALUES (0,3327,'Spaceship Command',8000,3,0,0)"
        Call ExecuteNonQuerySQL(Sql)
        Sql = "INSERT INTO CHARACTER_SKILLS VALUES (0,3381,'Amarr Frigate',2829,2,0,0)"
        Call ExecuteNonQuerySQL(Sql)
        Sql = "INSERT INTO CHARACTER_SKILLS VALUES (0,3330,'Caldari Frigate',2829,2,0,0)"
        Call ExecuteNonQuerySQL(Sql)
        Sql = "INSERT INTO CHARACTER_SKILLS VALUES (0,3328,'Gallente Frigate',2829,2,0,0)"
        Call ExecuteNonQuerySQL(Sql)
        Sql = "INSERT INTO CHARACTER_SKILLS VALUES (0,3329,'Minmatar Frigate',2829,2,0,0)"
        Call ExecuteNonQuerySQL(Sql)
        Sql = "INSERT INTO CHARACTER_SKILLS VALUES (0,3300,'Gunnery',1415,2,0,0)"
        Call ExecuteNonQuerySQL(Sql)
        Sql = "INSERT INTO CHARACTER_SKILLS VALUES (0,3303,'Small Energy Turret',8000,3,0,0)"
        Call ExecuteNonQuerySQL(Sql)
        Sql = "INSERT INTO CHARACTER_SKILLS VALUES (0,3301,'Small Hybrid Turret',8000,3,0,0)"
        Call ExecuteNonQuerySQL(Sql)
        Sql = "INSERT INTO CHARACTER_SKILLS VALUES (0,3302,'Small Projectile Turret',8000,3,0,0)"
        Call ExecuteNonQuerySQL(Sql)

    End Sub

End Class

Public Class EVESkill
    Public TypeID As Long
    Public Name As String
    Public Group As String
    Public SkillPoints As Long
    Public Level As Integer
    Public Overridden As Boolean
    Public OverriddenLevel As Integer
    Public PreReqSkills As EVESkillList

    Public Sub New()
        TypeID = 0
        Name = ""
        Group = ""
        SkillPoints = 0
        Level = 0
        Overridden = False
        OverriddenLevel = 0
        PreReqSkills = New EVESkillList
    End Sub

    Public Sub SetPreReqSkills()
        Dim SQL As String
        Dim PreReqs As New EVESkillList
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

        DBCommand = New SQLiteCommand(SQL, DB)
        readerSkills = DBCommand.ExecuteReader

        While readerSkills.Read
            With readerSkills
                TempSkill.TypeID = CInt(.GetDouble(0))
                TempSkill.Name = .GetString(1)
                TempSkill.Group = .GetString(2)
                TempSkill.Level = CInt(.GetValue(3))
                TempSkill.SkillPoints = 0
                TempSkill.OverriddenLevel = 0
                TempSkill.Overridden = False
                TempSkill.SetPreReqSkills()
            End With

            ' Set the local pre-reqs
            With TempSkill
                PreReqs.InsertSkill(.TypeID, .Level, .SkillPoints, .Overridden, .OverriddenLevel, .Name, .PreReqSkills, True)
            End With
        End While

        ' Save the final set
        PreReqSkills = PreReqs

        readerSkills.Close()
        readerSkills = Nothing
        DBCommand = Nothing

    End Sub

End Class