Imports System.Net
Imports System
Imports System.IO
Imports System.ComponentModel
Imports Shell32
Imports System.Diagnostics
Public Class Form1
    Public pass As Boolean = True
    Public dlcounter = 0
    Public completedlds As Boolean = True  'Οταν ξεκινησουνε τα downloads και για οσο διαρκουν παραμενει False
    Public i As Integer = 0
    Public advbool As Boolean = False
    Public prkbool As Boolean = False
    Public paybool As Boolean = False
    Public advenbool As Boolean = False
    Public advcampbool As Boolean = False
    Public advsxbool As Boolean = False
    Public advdstbool As Boolean = False
    Public useradmbool As Boolean = False
    Public sigtoolsbool As Boolean = False
    Public sharedpath As String
    Public advinstall As Boolean = False
    Public prkinstall As Boolean = False
    Public payinstall As Boolean = False
    Public adveninstall As Boolean = False
    Public advcampinstall As Boolean = False
    Public advsxinstall As Boolean = False
    Public advdstinstall As Boolean = False
    Public useradmininstall As Boolean = False
    Public sigtoolsinstall As Boolean = False
    Public close1 As Boolean = False
    Public path As String = Directory.GetCurrentDirectory
    Public progress As Dictionary(Of WebClient, Integer)
    Public libctrlsinstall As Boolean = False
    Public Event FileDownloadFailed(ByVal ex As Exception)
    Public payrbool As Boolean = False
    Private Sub Button1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
        If Not System.IO.File.Exists(path & "\AdvUpdMe.bat") Then
            'ifexists(path & "\update\AdvUpdMe.zip", path & "\update\AdvUpdMe.exe")
            My.Computer.Network.DownloadFile("http://www.signet.com.gr/PRImages/EditorImages/AdvUpdMe.bat", path & "\AdvUpdMe.bat")
        End If
        If Not System.IO.File.Exists(path & "\RegOnly-libcontrol32.bat") Then
            My.Computer.Network.DownloadFile("http://www.signet.com.gr/PRImages/EditorImages/RegOnly-libcontrol32.bat", path & "\RegOnly-libcontrol32.bat")
        End If
        'another comment
        RadioButton3.Enabled = False
        completedlds = False
        Label1.Visible = True
        Button1.Enabled = False
        RadioButton1.Enabled = False
        Button3.Enabled = False
        If RadioButton2.Checked Then
            If advbool = True Then
                ifexists(path & "\update\advance1.zip", path & "\update\advance1.exe")

                download("http://www.signet.com.gr/PRImages/EditorImages/advance1.zip", path & "\update\advance1.zip")
                dlcounter = dlcounter + 1
            End If
            If paybool = True Then
                ifexists(path & "\update\AdvancePay.zip", path & "\update\AdvancePay.exe")

                download("http://www.signet.com.gr/PRImages/EditorImages/AdvancePay.zip", path & "\update\AdvancePay.zip")
                dlcounter = dlcounter + 1
            End If
            If prkbool = True Then
                ifexists(path & "\update\advanceParK.zip", path & "\update\advanceParK.exe")

                download("http://www.signet.com.gr/PRImages/EditorImages/advanceParK.zip", path & "\update\advanceParK.zip")
                dlcounter = dlcounter + 1
            End If
            If advcampbool = True Then
                ifexists(path & "\update\advancecamp.zip", path & "\update\advancecamp.exe")
                download("http://www.signet.com.gr/PRImages/EditorImages/advancecamp.zip", path & "\update\advancecamp.zip")
                dlcounter = dlcounter + 1
            End If
            If advenbool = True Then
                ifexists(path & "\update\advance1en.zip", path & "\update\advance1en.exe")
                download("http://www.signet.com.gr/PRImages/EditorImages/advance1en.zip", path & "\update\advance1en.zip")
                dlcounter = dlcounter + 1
            End If
            If advdstbool = True Then
                ifexists(path & "\update\advanceDst.zip", path & "\update\advanceDst.exe")
                download("http://www.signet.com.gr/PRImages/EditorImages/advanceDst.zip", path & "\update\advanceDst.zip")
                dlcounter = dlcounter + 1
            End If
            If advsxbool = True Then
                ifexists(path & "\update\advanceSX.zip", path & "\update\advanceSX.exe")
                download("http://www.signet.com.gr/PRImages/EditorImages/advanceSX.zip", path & "\update\advanceSX.zip")
                dlcounter = dlcounter + 1
            End If
            If sigtoolsbool = True Then
                ifexists(path & "\update\sigtools1.zip", path & "\update\sigtools1.exe")
                download("http://www.signet.com.gr/PRImages/EditorImages/sigtools1.zip", path & "\update\sigtools1.zip")
                dlcounter = dlcounter + 1
            End If
            ifexists(path & "\update\ADVupdate.zip", path & "\update\ADVupdate.exe")
            download("http://www.signet.com.gr/PRImages/EditorImages/ADVupdate.zip", path & "\update\ADVupdate.zip")

            ifexists(path & "\update\LibControls.zip", path & "\update\LibControls.ocx")
            download("http://www.signet.com.gr/PRImages/EditorImages/LibControls.zip", path & "\update\LibControls.zip")
            dlcounter = dlcounter + 2
        End If

    End Sub
    Public Sub ifexists(zip As String, exe As String)
        'ΑΝ ΒΡΕΙ ΑΡΧΕΙΟ ΕΚΕΙ ΜΕΣΑ ΤΟ ΣΒΗΝΕΙ, ΜΙΑ ΠΟΥ ΤΟ DIRECTORY AYTO ΤΟ ΘΕΛΟΥΜΕ ΠΑΝΤΑ ΑΔΕΙΟ

        If System.IO.File.Exists(zip) Then
            System.IO.File.Delete(zip)
        End If
        If System.IO.File.Exists(exe) Then
            System.IO.File.Delete(exe)
        End If

    End Sub
    Public Sub download(uri As String, destination As String)
        Try
            Dim client As New WebClient
            progress = New Dictionary(Of WebClient, Integer)
            progress.Add(client, 0)
            AddHandler client.DownloadProgressChanged, AddressOf client_ProgressChanged
            AddHandler client.DownloadFileCompleted, AddressOf client_DownloadCompleted
            client.DownloadFileAsync(New Uri(uri), destination)
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
    Private Sub client_ProgressChanged(ByVal sender As Object, ByVal e As DownloadProgressChangedEventArgs)
        ' ProgressBar1.Value = e.ProgressPercentage
        progress(DirectCast(sender, WebClient)) = e.ProgressPercentage
        Dim total As Integer = 0
        For Each client In progress.Keys
            total += progress(client)
        Next
        ProgressBar1.Value = total / progress.Count
    End Sub

    Private Sub client_DownloadCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs)
        Label2.Visible = True
        i = i + 1 'i οι ληψεις που ολοκληρωθηκαν,dlcounter Οι ληψεις που επρεπε να γινουνε και progress.count οι ληψεις που εγιναν
        Label2.Text = "Λήψη " & i.ToString & " από " & dlcounter.ToString & " oλοκληρώθηκε"
        If dlcounter = i Then
            MessageBox.Show("Οι λήψεις ολοκληρώθηκαν")
            Label2.Visible = False
            Button2.Enabled = True
            ProgressBar1.Value = 0
            Label1.Visible = False
            Label2.Visible = False
            completedlds = True
            dlcounter = 0
            i = 0
        End If

    End Sub
    Private Sub Form1_FileDownloadFailed(ex As System.Exception) Handles Me.FileDownloadFailed
        MessageBox.Show("ΣΦΑΛΜΑ ΚΑΤΑ ΤΗ ΛΗΨΗ" & ex.Message, "!", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If completedlds = False Then
            Dim answer As DialogResult = MessageBox.Show("Επιλέξατε να τερματίσετε το πρόγραμμα, χωρίς να έχουν ολοκλρωθεί όλες οι λήψεις.", "ΠΡΟΣΟΧΗ!", MessageBoxButtons.OKCancel)
            If answer = Windows.Forms.DialogResult.Cancel Then
                e.Cancel = True
            End If
        End If
        If close1 = True Then
            System.Diagnostics.Process.Start(path & "\AdvUpdMe.bat")
            'System.Diagnostics.Process.Start(path & "\AdvUpdMe.exe")
            Me.Dispose()
        End If
    End Sub

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        RadioButton2.Checked = True
        Try
            ' Open the file using a stream reader.
            Using sr As New StreamReader(path & "\ADVupdate.ini")
                Dim line As String
                ' Read the stream to a string and write the string to the console.

                Do While sr.Peek() <> -1

                    line = sr.ReadLine()

                    If line.Contains("prog=") Then
                        Dim l As String = line.Trim()
                        Dim length As Integer = l.Length
                        Dim index As Integer = l.IndexOf("=")

                        Dim program As String = l.Substring(index + 1, length - index - 1)

                        If program = "advance1" Then
                            killer("ADVANCE1")
                            advbool = True
                            '  Form2.CheckedListBox1.Items.Add("ADVANCE")
                        ElseIf program = "AdvancePay" Then
                            killer("AdvancePay")
                            paybool = True
                            If System.IO.Directory.Exists(path & "\Payr") = True Then
                                payrbool = True
                            End If
                            '     Form2.CheckedListBox1.Items.Add("AdvancePay")
                        ElseIf program = "advanceParK" Then
                            killer("advanceParK")
                            prkbool = True
                            'Form2.CheckedListBox1.Items.Add("advanceParK")
                        ElseIf program = "advancecamp" Then
                            killer("advancecamp")
                            advcampbool = True
                            '   Form2.CheckedListBox1.Items.Add("advancecamp")
                        ElseIf program = "advance1en" Then
                            killer("advance1en")
                            advenbool = True
                            '    Form2.CheckedListBox1.Items.Add("advance1en")
                        ElseIf program = "advanceSX" Then
                            killer("advanceSX")
                            advsxbool = True
                            ' Form2.CheckedListBox1.Items.Add("advanceSX")
                        ElseIf program = "advanceDst" Then
                            killer("advanceDst")
                            advdstbool = True
                            '    Form2.CheckedListBox1.Items.Add("advanceDst")
                        ElseIf program = "sigtools1" Then
                            killer("sigtools1")
                            sigtoolsbool = True
                            '    Form2.CheckedListBox1.Items.Add("sigtools1")
                        ElseIf program = "UserAdministration" Then
                            killer("UserAdministration")
                            useradmbool = True

                        End If

                    End If
                Loop
                ' Form2.CheckedListBox1.Items.Add("libcontrols")
            End Using
        Catch ex As Exception

            MessageBox.Show(ex.Message)
        End Try

        If (Not System.IO.Directory.Exists(path & "\update")) Then 'AN ΔΕ ΒΡΕΙ DIRECTORY ΓΙΑ update TO ΦΤΙΑΧΝΕΙ

            System.IO.Directory.CreateDirectory(path & "\update")
        End If
        If (Not System.IO.Directory.Exists(path & "\update\container")) Then

            System.IO.Directory.CreateDirectory(path & "\update\container")
        End If

    End Sub
    Private Sub killer(app As String)
        Dim p() As Process
        Dim PS As Process
        p = Process.GetProcessesByName(app)
        If p.Count > 0 Then                       'ΕΛΕΓΧΕΙ ΑΝ ΤΡΕΧΕΙ ΤΟ ADVANCE ΚΑΙ ΑΝΑΛΟΓΑ ΤΙ ΘΕΛΕΙ Ο ΧΡΗΣΤΗΣ ΤΟ ΚΛΕΙΝΕΙ Η' ΚΛΕΙΝΕΙ ΤΟ 
            ''''''''''''''''''''''''''''''''''''''''''TΡΕΧΟΝ ΠΡΟΓΡΑΜΜΑ ΠΡΟΚΕΙΜΕΝΟΥ Ο ΧΡΗΣΤΗΣ ΝΑ ΤΕΛΕΙΩΣΕΙ ΤΗ ΔΟΥΛΕΙΑ ΤΟΥ
            Dim RESULT As Integer = MessageBox.Show("Το πρόγραμμα αυτό θα τερματίσει τη λειτουργία του " & app & "!" & vbCrLf & "Aν έχετε κάποια εκκρεμή εργασία ολοκληρώστε" & vbCrLf & " και κατόπιν επανεκκινείστε το πρόγραμμα αναβάθμισης." & vbCrLf & vbCrLf & "Θέλετε να προχωρήσετε στον τερματισμό του " & app & ";", "ΠΡΟΣΟΧΗ", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
            If RESULT = 7 Then
                Me.Close()
            ElseIf RESULT = 6 Then
                For Each PS In p
                    PS.Kill()

                Next
            End If
        End If
    End Sub

    Private Sub unzip(inputfile As String)
        'Dim sc As New Shell32.Shell()  'ΑΥΤΗ Η ΕΝΤΟΛΗ ΔΕΝ ΤΡΕΧΕΙ ΣΤΑ ΧΡ. ΤΗΝ ΑΝΤΙΚΑΘΙΣΤΟΥΜΕ ΜΕ ΤΙΣ 2 ΑΠΟ ΚΑΤΩ ΓΡΑΜΜΕΣ ΚΑΙ ΤΡΕΧΕΙ
        Try
            Dim ShellAppType As Type = Type.GetTypeFromProgID("Shell.Application")
            Dim sc As Object = Activator.CreateInstance(ShellAppType)
            Dim output As Shell32.Folder = sc.NameSpace(path & "\update")

            Dim input As Shell32.Folder = sc.NameSpace(path & inputfile)
            output.CopyHere(input.Items, 256) 'Η ΕΝΤΟΛΗ ΑΥΤΗ ΚΑΝΕΙ ΑΠΟΣΥΜΠΙΕΣΗ ΕΜΦΑΝΙΖΟΝΤΑΣ ΤΑΥΤΟΧΡΟΝΑ
            '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''ΚΑΙ ΤΟ PROGRESS ΤΗΣ ΑΠΟΣΥΜΠΙΕΣΗΣ.ΑΥΤΟ ΚΑΝΕΙ Η ΠΑΡΑΜΕΝΤΡΟΣ 256
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub
    Private Sub renamenreplace(folder As String, prg As String, kataliksi As String)
        Try
            If System.IO.File.Exists(path & "\update\container\" & prg & kataliksi) Then
                Dim inforeader3 As System.IO.FileInfo = My.Computer.FileSystem.GetFileInfo(path & "\update\container\" & prg & kataliksi)
                Dim D As Date = inforeader3.LastWriteTime
                Dim S1 As String = D.ToShortDateString
                Dim S2 As String = S1.Replace("/", "-")
                If Not System.IO.File.Exists(path & folder & "\" & prg & "_" & S2 & kataliksi) Then
                    My.Computer.FileSystem.RenameFile(path & "\update\container\" & prg & kataliksi, prg & "_" & S2 & kataliksi)
                    File.Copy(path & "\update\container\" & prg & "_" & S2 & kataliksi, path & folder & "\" & prg & "_" & S2 & kataliksi)
                    System.IO.File.Delete(path & "\update\container\" & prg & "_" & S2 & kataliksi)
                Else
                    My.Computer.FileSystem.RenameFile(path & "\update\container\" & prg & kataliksi, prg & "_" & S2 & "A" & kataliksi)
                    File.Copy(path & "\update\container\" & prg & "_" & S2 & "A" & kataliksi, path & folder & "\" & prg & "_" & S2 & "A" & kataliksi)
                    System.IO.File.Delete(path & "\update\container\" & prg & "_" & S2 & "A" & kataliksi)
                End If

            End If
            System.IO.File.Copy(path & folder & "\" & prg & kataliksi, path & "\update\container\" & prg & kataliksi)
            System.IO.File.Delete(path & folder & "\" & prg & kataliksi)
            System.IO.File.Copy(path & "\update\" & prg & kataliksi, path & folder & "\" & prg & kataliksi)
            System.IO.File.Delete(path & "\update\" & prg & kataliksi)
            MessageBox.Show("Τo " & prg & " αναβαθμίστηκε επιτυχώς!")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
    
    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Button2.Enabled = False
        CheckBox1.Enabled = False
        If RadioButton2.Checked Then
            'If Not System.IO.File.Exists(path & "\AdvUpdMe.exe") Then
            '    unzip("\update\AdvUpdMe.zip")
            '    System.IO.File.Copy(path & "\update\AdvUpdMe.exe", path & "\AdvUpdMe.exe")
            'End If
            If advbool = True Then

                unzip("\update\advance1.zip")
                wewe("\", "", "advance1", ".exe")

                'If inforeader.LastWriteTime <> inforeader2.LastWriteTime Or inforeader.Length <> inforeader2.Length Then     'ΕΛΕΓΧΕΙ ΤΑ LAST MODIFIED TIME ΤΩΝ 2 ΑΡΧΕΙΩΝ ΚΑΙ ΑΝ ΤΑ ΒΡΕΙ 
                '    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''   'ΔΙΑΦΟΡΕΤΙΚΑ ΑΛΛΑΖΕΙ ΤΟ ΟΝΟΜΑ ΤΟΥ ΠΡΩΤΟΥ ΚΑΙ ΑΝΤΙΓΡΑΦΕΙ
                '    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''ΤΟ ΚΑΙΝΟΥΡΙΟ ΣΤΗ ΘΕΣΗ ΤΟΥ ΠΑΛΙΟΥ
                '    '    Try              ΥΠΟΔΕΙΓΜΑ RENAMENREPLACE ΓΙΑ ADVANCE

                '    '        If System.IO.File.Exists(path & "\update\container\advance1.exe") Then
                '    '            Dim inforeader3 As System.IO.FileInfo = My.Computer.FileSystem.GetFileInfo(path & "\update\container\advance1.exe")
                '    '            Dim D As Date = inforeader3.LastWriteTime
                '    '            Dim S1 As String = D.ToShortDateString
                '    '            Dim S2 As String = S1.Replace("/", "-")
                '    '            My.Computer.FileSystem.RenameFile(path & "\update\container\advance1.exe", "advance1" & "_" & S2 & ".exe")
                '    '            File.Copy(path & "\update\container\advance1" & "_" & S2 & ".exe", path & "\advance1" & "_" & S2 & ".exe")
                '    '            System.IO.File.Delete(path & "\update\container\advance1" & "_" & S2 & ".exe")
                '    '        End If
                '    '        System.IO.File.Copy(path & "\advance1.exe", path & "\update\container\advance1.exe")
                '    '        System.IO.File.Delete(path & "\advance1.exe")
                '    '        System.IO.File.Copy(path & "\update\advance1.exe", path & "\advance1.exe")
                '    '        System.IO.File.Delete(path & "\update\advance1.exe")
                '    '        MessageBox.Show("ΤΟ ADVANCE ΑΝΑΒΑΘΜΙΣΤΗΚΕ ΕΠΙΤΥΧΩΣ")
                '    '        Me.Close()
                '    '    Catch copyError As IOException
                '    '        MessageBox.Show(copyError.Message)
                '    '    End Try
                '    renamenreplace("", "advance1", ".exe")
                'Else
                '    MessageBox.Show("Tο advance είναι ενημερωμένο")
                '    System.IO.File.Delete(path & "\update\advance1.exe")

                'End If
            End If
            If (paybool = True And payrbool = False) Then

                unzip("\update\AdvancePay.zip")

                If System.IO.Directory.Exists(path & "\Pay") = True Then
                    wewe("", "\Pay\", "AdvancePay", ".exe")

                Else
                    wewe("\", "", "AdvancePay", ".exe")

                End If
            End If
            If payrbool = True Then
                unzip("\update\AdvancePay.zip")
                wewe("", "\Payr\", "AdvancePay", ".exe")

            End If
            'End If

            '    Try               YΠΟΔΕΙΓΜΑ SUB RENAMENREPLACE ΓΙΑ ΑDVANCE PAY

            '        If System.IO.File.Exists(path & "\update\container\AdvancePay.exe") Then
            '            Dim inforeader3 As System.IO.FileInfo = My.Computer.FileSystem.GetFileInfo(path & "\update\container\AdvancePay.exe")
            '            Dim D As Date = inforeader3.LastWriteTime
            '            Dim S1 As String = D.ToShortDateString
            '            Dim S2 As String = S1.Replace("/", "-")
            '            My.Computer.FileSystem.RenameFile(path & "\update\container\AdvancePay.exe", "AdvancePay" & "_" & S2 & ".exe")
            '            File.Copy(path & "\update\container\AdvancePay" & "_" & S2 & ".exe", path & "\Pay\AdvancePay" & "_" & S2 & ".exe")
            '            System.IO.File.Delete(path & "\update\container\AdvancePay" & "_" & S2 & ".exe")
            '        End If
            '        System.IO.File.Copy(path & "\Pay\AdvancePay.exe", path & "\update\container\AdvancePay.exe")
            '        System.IO.File.Delete(path & "\Pay\AdvancePay.exe")
            '        System.IO.File.Copy(path & "\update\AdvancePay.exe", path & "\Pay\AdvancePay.exe")
            '        System.IO.File.Delete(path & "\update\AdvancePay.exe")
            '        MessageBox.Show("Η ΕΦΑΡΜΟΓΗ ΜΙΣΘΟΔΟΣΙΑΣ ΑΝΑΒΑΘΜΙΣΤΗΚΕ ΕΠΙΤΥΧΩΣ")
            '        Me.Close()
            '    Catch copyError As IOException
            '        MessageBox.Show(copyError.Message)
            '    End Try

            If prkbool = True Then

                unzip("\update\advanceParK.zip")
                If System.IO.Directory.Exists(path & "\Prk") Then
                    wewe("", "\Prk\", "advanceParK", ".exe")

                Else
                    wewe("\", "", "advanceParK", ".exe")

                End If
            End If
            If advenbool = True Then 'ΥΠΟΔΕΙΓΜΑ WEWE

                unzip("\update\advance1en.zip")
                If System.IO.Directory.Exists(path & "\Endyma") Then
                    wewe("", "\Endyma\", "advance1en", ".exe")
                    'Dim inforeader As System.IO.FileInfo = My.Computer.FileSystem.GetFileInfo(path & "\update\advance1en.exe")

                    'Dim inforeader2 As System.IO.FileInfo = My.Computer.FileSystem.GetFileInfo(path & "\Endyma\advance1en.exe")
                    'If inforeader.Length <= inforeader2.Length Then 'ελεγχει αν η διαφορα μεγεθους των 2 αρχειων ειναι> 10 kb για λογους ασφαλειας.ΑΝ ειναι σταματαει το προγραμμα
                    '    message1("advance1en")
                    'End If
                    'If inforeader2.LastWriteTime > inforeader.LastWriteTime Then
                    '    message2("advance1en")
                    'End If
                    'If inforeader.LastWriteTime <> inforeader2.LastWriteTime Or inforeader.Length <> inforeader2.Length Then     'ΕΛΕΓΧΕΙ ΤΑ LAST MODIFIED TIME ΤΩΝ 2 ΑΡΧΕΙΩΝ ΚΑΙ ΑΝ ΤΑ ΒΡΕΙ 

                    '    renamenreplace("\Endyma\", "advance1en", ".exe")
                    'Else
                    '    MessageBox.Show("Η εφαρμογή Advance1en είναι ενημερωμένη")
                    '    System.IO.File.Delete(path & "\update\advance1en.exe")

                    'End If
                Else
                    wewe("\", "", "advance1en", ".exe")
                    '        Dim inforeader As System.IO.FileInfo = My.Computer.FileSystem.GetFileInfo(path & "\update\advance1en.exe")

                    '        Dim inforeader2 As System.IO.FileInfo = My.Computer.FileSystem.GetFileInfo(path & "\advance1en.exe")
                    '        If inforeader.Length <= inforeader2.Length Then 'ελεγχει αν η διαφορα μεγεθους των 2 αρχειων ειναι> 10 kb για λογους ασφαλειας.ΑΝ ειναι σταματαει το προγραμμα
                    '            message1("advance1en")
                    '        End If
                    '        If inforeader2.LastWriteTime > inforeader.LastWriteTime Then
                    '            message2("advance1en")
                    '        End If
                    '        If inforeader.LastWriteTime <> inforeader2.LastWriteTime Or inforeader.Length <> inforeader2.Length Then
                    '            renamenreplace("", "advance1en", ".exe")
                    '        Else
                    '            MessageBox.Show("Η εφαρμογή Advance1en είναι ενημερωμένη")
                    '            System.IO.File.Delete(path & "\update\advance1en.exe")

                    '        End If
                End If
            End If
            If advsxbool = True Then

                unzip("\update\advanceSX.zip")
                If System.IO.Directory.Exists(path & "\Esex") Then

                    wewe("", "\Esex\", "advanceSX", ".exe")
                Else
                    wewe("\", "", "advanceSX", ".exe")

                End If
            End If
            If advcampbool = True Then

                unzip("\update\advance1en.zip")
                If System.IO.Directory.Exists(path & "\Travel") Then

                    wewe("", "\Travel\", "advancecamp", ".exe")

                Else
                    wewe("\", "", "advancecamp", ".exe")

                End If

            End If
            If advdstbool = True Then
                unzip("\update\advanceDst.zip")
                wewe("\", "", "advanceDst", ".exe")

            End If
            If useradmbool = True Then
                unzip("\update\UserAdministration.zip")
                wewe("\", "", "UserAdministration", ".exe")

            End If

            If sigtoolsbool = True Then
                unzip("\update\sigtools1.zip")
                wewe("\", "", "sigtools1", ".exe")

            End If
            If True Then
                unzip("\update\LibControls.zip")
                Dim inforeader As System.IO.FileInfo = My.Computer.FileSystem.GetFileInfo(path & "\update\LibControls.ocx")

                Dim inforeader2 As System.IO.FileInfo = My.Computer.FileSystem.GetFileInfo(path & "\LibControls.ocx")
                If inforeader.Length < inforeader2.Length Then 'ελεγχει αν η διαφορα μεγεθους των 2 αρχειων ειναι> 10 kb για λογους ασφαλειας.ΑΝ ειναι σταματαει το προγραμμα
                    message1("LibControls")

                End If
                If inforeader2.LastWriteTime > inforeader.LastWriteTime And pass = True And Math.Abs(inforeader.LastWriteTime.Hour - inforeader2.LastWriteTime.Hour) <> 1 Then
                    message2("LibControls")
                End If
                If ((inforeader.LastWriteTime <> inforeader2.LastWriteTime) Or (inforeader.Length <> inforeader2.Length)) And ((inforeader.LastWriteTime.Date <> inforeader2.LastWriteTime.Date) Or (Math.Abs(inforeader.LastWriteTime.Hour - inforeader2.LastWriteTime.Hour) <> 1)) Then

                    If pass = True Then
                        renamenreplace("", "LibControls", ".ocx")
                        If CheckBox1.Checked = False Then
                            runbatfile("\RegOnly-libcontrol32.bat")
                        End If
                    Else
                        MessageBox.Show("Eπιλέξατε να μη γίνει αναβάθμιση του LibControls.")

                    End If

                Else
                    MessageBox.Show("Το LibControls είναι ενημερωμένο.")
                End If
                pass = True
                Me.Focus()
            End If
            If True Then
                unzip("\update\ADVupdate.zip")
                Dim inforeader As System.IO.FileInfo = My.Computer.FileSystem.GetFileInfo(path & "\update\ADVupdate.exe")

                Dim inforeader2 As System.IO.FileInfo = My.Computer.FileSystem.GetFileInfo(path & "\ADVupdate.exe")
                If inforeader.Length < inforeader2.Length Then 'ελεγχει αν η διαφορα μεγεθους των 2 αρχειων ειναι> 10 kb για λογους ασφαλειας.ΑΝ ειναι σταματαει το προγραμμα
                    message1("ADVupdate")
                End If
                If inforeader2.LastWriteTime > inforeader.LastWriteTime And pass = True Then
                    message2("ADVupdate")
                End If
                If ((inforeader.LastWriteTime <> inforeader2.LastWriteTime) Or (inforeader.Length <> inforeader2.Length)) Then

                    If pass = True Then
                        close1 = True
                        Me.Close()
                    Else
                        MessageBox.Show("Eπιλέξατε να μη γίνει αναβάθμιση του ADVupdate.")

                    End If

                Else
                    MessageBox.Show("Η εφαρμογή ADVupdate είναι ενημερωμένη")
                    System.IO.File.Delete(path & "\update\ADVupdate.exe")

                End If
                pass = True
                Me.Focus()
            End If
            RadioButton1.Enabled = True
            CheckBox1.Enabled = True
        End If
        If RadioButton1.Checked Then
            If advinstall = True Then
                unzip("\update\advance1.zip")
                copypaste("", "advance1")
            End If
            If payinstall = True Then

                If (paybool = True And payrbool = False) Then
                    unzip("\update\AdvancePay.zip")
                    copypaste("Pay\", "AdvancePay")
                End If
                If payrbool = True Then
                    Try
                        unzip("\update\AdvancePay.zip")
                        System.IO.File.Copy(path & "\update\AdvancePay.exe", path & "\Payr\AdvancePay.exe")
                        System.IO.File.Delete(path & "\update\AdvancePay.exe")
                        MessageBox.Show("Τo AdvancePay εγκαταστάθηκε επιτυχώς!")
                    Catch ex As Exception
                        MessageBox.Show(ex.ToString)
                    End Try
                End If

            End If
            If prkinstall = True Then
                unzip("\update\advanceParK.zip")
                copypaste("Prk\", "advanceParK")
            End If
            If adveninstall = True Then
                unzip("\update\advance1en.zip")
                copypaste("Endyma\", "advance1en")
            End If
            If advsxinstall = True Then
                unzip("\update\advanceSX.zip")
                copypaste("Esex\", "advanceSX")
            End If
            If advdstinstall = True Then
                unzip("\update\advanceDst.zip")
                copypaste("", "advanceDst")
            End If
            If advcampinstall = True Then
                unzip("\update\advancecamp.zip")
                copypaste("", "advancecamp")
            End If
            If useradmininstall = True Then
                unzip("\update\UserAdministration.zip")
                copypaste("", "UserAdministration")
            End If
            If sigtoolsinstall = True Then
                unzip("\update\sigtools1.zip")
                copypaste("", "sigtools1")
            End If

            If libctrlsinstall = True Then
                unzip("\update\LibControls.zip")
                Try
                    System.IO.File.Copy(path & "\update\LibControls.ocx", path & "\LibControls.ocx")
                    System.IO.File.Delete(path & "\update\LibControls.ocx")
                    runbatfile("\RegOnly-libcontrol32.bat")

                    MessageBox.Show("Τo LibControls εγκαταστάθηκε επιτυχώς!")

                Catch ex As Exception
                    MessageBox.Show(ex.ToString)
                End Try
            End If
        End If
        Button3.Enabled = True
        RadioButton1.Enabled = True
        RadioButton2.Enabled = True
        RadioButton3.Enabled = True

    End Sub
    Private Sub runbatfile(file1 As String)
        Try     'run as administrator
            Dim procInfo As New ProcessStartInfo()
            procInfo.UseShellExecute = True
            procInfo.FileName = (path & file1)
            procInfo.WorkingDirectory = ""
            procInfo.Verb = "runas"
            Process.Start(procInfo)
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        End Try
    End Sub
    Private Sub copypaste(fol As String, programme As String)
        Try
            If System.IO.Directory.Exists(path & "\" & fol) = True Then
                System.IO.File.Copy(path & "\update\" & programme & ".exe", path & "\" & fol & programme & ".exe")
                System.IO.File.Delete(path & "\update" & programme & ".exe")

            Else
                System.IO.File.Copy(path & "\update\" & programme & ".exe", path & "\" & programme & ".exe")
                System.IO.File.Delete(path & "\update\" & programme & ".exe")
            End If
            MessageBox.Show("Τo " & programme & " εγκαταστάθηκε επιτυχώς!")
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
        Me.Focus()
    End Sub

    Private Sub wewe(slash As String, fld As String, appl As String, katal As String)
        If System.IO.Directory.Exists(path & fld) Then
            Dim inforeader As System.IO.FileInfo = My.Computer.FileSystem.GetFileInfo(path & "\update\" & appl & katal)

            Dim inforeader2 As System.IO.FileInfo = My.Computer.FileSystem.GetFileInfo(path & slash & fld & appl & katal)
            If inforeader.Length < inforeader2.Length Then 'ελεγχει αν η διαφορα μεγεθους των 2 αρχειων ειναι> 10 kb για λογους ασφαλειας.ΑΝ ειναι σταματαει το προγραμμα
                message1(appl)
            End If
            If inforeader2.LastWriteTime > inforeader.LastWriteTime And Math.Abs(inforeader.LastWriteTime.Hour - inforeader2.LastWriteTime.Hour) <> 1 And pass = True Then
                message2(appl)
                'Σε περιπτωση που απαντησουμε οχι στα messageboxes των message1 και message2 αν βρει το ιδιο αρχειο θα παει κατευθειαν στο "η εφαρμογη ειναι ενημερωμενη 
                'και οχι στο μηνυμα επιλεξατε να μη γινει αναβαθμιση αφου εχοντας βρει το ιδιο αρχειο δε μπαινει ποτε σε εκεινο το κομματι του κωδικα
            End If
            If (inforeader.LastWriteTime <> inforeader2.LastWriteTime Or inforeader.Length <> inforeader2.Length) And (inforeader.LastWriteTime.Date <> inforeader2.LastWriteTime.Date Or Math.Abs(inforeader.LastWriteTime.Hour - inforeader2.LastWriteTime.Hour) <> 1) Then     'ΕΛΕΓΧΕΙ ΤΑ LAST MODIFIED TIME ΤΩΝ 2 ΑΡΧΕΙΩΝ ΚΑΙ ΑΝ ΤΑ ΒΡΕΙ 
                If pass = True Then
                    renamenreplace(fld, appl, katal)
                Else
                    MessageBox.Show("Eπιλέξατε να μη γίνει αναβάθμιση του " & appl)
                End If

            Else
                MessageBox.Show("Η εφαρμογή " & appl & " είναι ενημερωμένη.")
                System.IO.File.Delete(path & "\update\" & appl & katal)

            End If
        End If
        Me.Focus()
        pass = True
    End Sub
    Private Sub message1(programma As String)
        Dim answer As DialogResult = MessageBox.Show("Η νέα έκδοση του προγράμματος φαίνεται να είναι μικρότερη σε μέγεθος από την παλιά." & vbCrLf & "Είστε σίγουρος οτι θέλετε να συνεχίσετε;", programma & " -ΠΡΟΣΟΧΗ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If answer = Windows.Forms.DialogResult.Yes Then
        ElseIf answer = Windows.Forms.DialogResult.No Then
            pass = False

        End If
        Me.Focus()
    End Sub
    Private Sub message2(PR As String)
        Dim answer As DialogResult = MessageBox.Show("Η έκδοση του προγράμματος που επιχειρείτε να εγκαταστήστε είναι παλαιότερη της τρέχουσας." & vbCrLf & "Είστε σίγουρος οτι θέλετε να συνεχίσετε;", PR & " -ΠΡΟΣΟΧΗ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
        If answer = Windows.Forms.DialogResult.Yes Then
        ElseIf answer = Windows.Forms.DialogResult.No Then

            pass = False
        End If
        Me.Focus()
    End Sub

    Private Sub Button3_Click(sender As System.Object, e As System.EventArgs) Handles Button3.Click
        If RadioButton1.Checked Then
            Button1.Enabled = False
            Form2.ShowDialog()
        End If
        If RadioButton2.Checked Then
            Button1.Enabled = True

        End If
        'ΑΥΤΟ ΕΙΝΑΙ ΓΙΑ ΤΗΝ ΠΕΡΙΠΤΩΣΗ ΠΟΥ ΤΟ ΚΑΙΝΟΥΡΙΟ .ΕΧΕ ΕΧΕΙ ΘΕΜΑ ΚΑΙ ΔΕ ΔΟΥΛΕΥΕΙ ΚΑΙ ΘΕΛΟΥΜΕ ΝΑ ΚΑΝΟΥΜΕ ROLLBACK ΣΤΗΝ ΠΡΟΗΓΟΥΜΕΝΗ 
        'VERSION TOY.ΕΔΩ ΘΑ ΠΡΕΠΕΙ ΝΑ ΤΡΟΠΟΠΟΙΗΣΟΥΜΕ ΚΑΙ ΤΟΝ ΠΑΡΑΠΑΝΩ ΚΩΔΙΚΑ ΚΑΙ ΤΗΝ ΟΛΗ ΔΙΑΔΙΙΚΑΣΙΑ ΤΟΥ ΗΛΙΑ ΩΣ ΕΞΗΣ. ΧΡΗΣΜΙΠΟΙΟΥΜΕ
        'ΕΝΑ ΝΕΟ ΦΑΚΕΛΟ ΤΟΝ CONTAINER.ΕΚΕΙ ΘΑ ΦΥΛΑΣΣΕΤΑΙ Η ΤΕΛΕΥΤΑΙΑ version ΤΟΥ .ΕΧΕ ΠΟΥ ΒΕΒΑΙΩΜΕΝΑ ΤΡΕΧΕΙ.ΤΟ ΟΝΟΜΑ ΘΑ ΠΑΡΑΜΕΝΕΙ ADVANCE.
        'ΤΟ ΚΟΥΜΠΙ ROLLBACK ΘΑ ΣΒΗΝΕΙ ΤΟ ADVANCE ΑΠΟ ΤΟ ΦΑΚΕΛΟ iNSTALL,ΘΑ ΑΝΤΙΓΡΑΦΕΙ ΤΟ ADVANCE ΑΠΟ ΤΟ ΦΑΚΕΛΟ CONTAINER ΣΤΟ ΦΑΚΕΛΟ 
        'INSTALL KAI ΘΑ ΔΙΑΓΡΑΦΕΙ ΤΟ ADVANCE ΑΠΟ ΤΟ ΦΑΚΕΛΟ CONTAINER.
        If RadioButton3.Checked Then
            Dim answer As MsgBoxResult = MessageBox.Show("Αν συνεχίσετε θα διαγραφούν αρχεία." & vbCrLf & "Eίστε σίγουρος ότι θέλετε να συνεχίσετε ;", "ΠΡΟΣΟΧΗ!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
            If answer = MsgBoxResult.Yes Then
                'Try  YΠΟΔΕΙΓΜΑ ROLLBACK
                '    System.IO.File.Delete(path & "\advance1.exe")
                '    If System.IO.File.Exists(path & "\update\container\advance1.exe") Then
                '        System.IO.File.Copy(path & "\update\container\advance1.exe", path & "\advance1.exe")
                '        System.IO.File.Delete(path & "\update\container\advance1.exe")
                '        MessageBox.Show("H ΔΙΑΔΙΚΑΣΙΑ ΤΟΥ ROLLBACK ΕΚΤΕΛΕΣΘΗΚΕ ΕΠΙΤΥΧΩΣ!")

                '    End If
                'Catch EX As Exception
                '    MessageBox.Show(EX.ToString)
                'End Try

                Form2.ShowDialog()
            End If

        End If
        
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As System.EventArgs) Handles RadioButton2.CheckedChanged

        If RadioButton1.Checked = True Or RadioButton3.Checked = True Then
            Button1.Enabled = False
            CheckBox1.Checked = False
            CheckBox1.Enabled = False
        End If

    End Sub

    Private Sub RadioButton2_Click(sender As Object, e As System.EventArgs) Handles RadioButton2.Click
        CheckBox1.Enabled = True
    End Sub
End Class