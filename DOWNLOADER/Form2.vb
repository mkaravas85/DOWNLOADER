Public Class Form2

    Private Sub rollback(pathapp As String, folderpath As String)
        Dim char1 As Char() = {"\"}
        Dim name1 As String = pathapp.TrimStart(char1)
        Dim length As Integer = name1.Length
        Dim index As Integer = name1.IndexOf(".")

        Dim program As String = name1.Substring(0, index)
        Try

            If System.IO.File.Exists(Form1.path & "\update\container" & pathapp) Then
                System.IO.File.Delete(Form1.path & folderpath & pathapp)
                System.IO.File.Copy(Form1.path & "\update\container" & pathapp, Form1.path & folderpath & pathapp)
                System.IO.File.Delete(Form1.path & "\update\container" & pathapp)

                MessageBox.Show("H διαδικασία του rollback για την εφαρμογη " & program & " εκτελέσθηκε επιτυχώς!")
            Else
                MessageBox.Show("Δεν υπάρχει πρότερη έκδοση για την εφαρμογή " & program)
            End If

        Catch EX As Exception
            MessageBox.Show(EX.ToString)
        End Try
    End Sub
    
    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        If CheckedListBox1.CheckedItems.Contains("advance1") Then
            rollback("\advance1.exe", "")

        End If
        If (CheckedListBox1.CheckedItems.Contains("AdvancePay") And Form1.payrbool = False) Then
            If System.IO.Directory.Exists(Form1.path & "\Pay") Then
                rollback("\AdvancePay.exe", "\Pay")
            Else
                rollback("\AdvancePay.exe", "")
            End If
        End If
        If (CheckedListBox1.CheckedItems.Contains("AdvancePay") And Form1.payrbool = True) Then
            rollback("\AdvancePay.exe", "\Payr")
        End If

        If CheckedListBox1.CheckedItems.Contains("advanceParK") Then
            If System.IO.Directory.Exists(Form1.path & "\Prk") Then
                rollback("\advanceParK.exe", "\Prk")
            Else
                rollback("\advanceParK.exe", "")
            End If
        End If
        If CheckedListBox1.CheckedItems.Contains("advancecamp") Then
            
                rollback("\advancecamp.exe", "")
        End If
        If CheckedListBox1.CheckedItems.Contains("advance1en") Then
            If System.IO.Directory.Exists(Form1.path & "\Endyma") Then
                rollback("\advance1en.exe", "\Endyma")
            Else
                rollback("\advance1en.exe", "")
            End If
        End If
        If CheckedListBox1.CheckedItems.Contains("advanceSX") Then
            If System.IO.Directory.Exists(Form1.path & "\Esex") Then
                rollback("\advanceSX.exe", "\Esex")
            Else
                rollback("\advanceSX.exe", "")
            End If
        End If
        If CheckedListBox1.CheckedItems.Contains("advanceDst") Then

            rollback("\advanceDst.exe", "")
        End If
        If CheckedListBox1.CheckedItems.Contains("UserAdministration") Then
            rollback("\UserAdministration.exe", "")

        End If
        If CheckedListBox1.CheckedItems.Contains("sigtools1") Then
            rollback("\sigtools1.exe", "")

        End If
        If CheckedListBox1.CheckedItems.Contains("LibControls") Then
            rollback("\LibControls.ocx", "")

        End If

        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub Form2_FormClosing(sender As Object, e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me.Dispose()
    End Sub


    Private Sub Form2_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        chklstadd(Form1.advbool, "advance1")
        chklstadd(Form1.paybool, "AdvancePay")
        chklstadd(Form1.prkbool, "advanceParK")
        chklstadd(Form1.advcampbool, "advancecamp")
        chklstadd(Form1.advenbool, "advance1en")
        chklstadd(Form1.advsxbool, "advanceSX")
        chklstadd(Form1.advdstbool, "advanceDst")
        chklstadd(Form1.sigtoolsbool, "sigtools1")
        chklstadd(Form1.useradmbool, "User Administration")

        Form1.dlcounter = 0
        CheckedListBox1.Items.Add("LibControls")
        If Form1.RadioButton3.Checked Then
            Label1.Text = "Επιλέξτε ποιές απο τις παρακάτω εφαρμογές" & vbCrLf & "θέλετε να επαναφέρετε σε πρότερη έκδοση."
            Button2.Enabled = False
            Button1.Enabled = True
        End If
        If Form1.RadioButton1.Checked Then
            Label1.Text = "Επιλέξτε ποιές απο τις παρακάτω εφαρμογές" & vbCrLf & "θέλετε να εγκαταστήσετε:"
            Button2.Enabled = True
            Button1.Enabled = False
        End If
    End Sub
    Private Sub chklstadd(bool1 As Boolean, pr As String)
        If bool1 = True Then

            CheckedListBox1.Items.Add(pr)
        End If
    End Sub
    Private Sub checkndownload(prog As String, folder As String, uri As String, destpath As String, ByRef bool As Boolean)
        If CheckedListBox1.CheckedItems.Contains(prog) Then
            If (System.IO.File.Exists(Form1.path & folder & prog & ".exe") Or System.IO.File.Exists(Form1.path & "\" & prog & ".exe")) Then
                MessageBox.Show("Το αρχείο " & prog & ".exe υπάρχει ήδη. Επιλέξτε Update από την πρώτη φόρμα")
            Else
                Form1.ifexists(destpath, Form1.path & "\update\" & prog & ".exe")
                Form1.download(uri, destpath)
                Form1.dlcounter = Form1.dlcounter + 1
                bool = True
                Form1.Label1.Visible = True
            End If
        End If

    End Sub
    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Form1.advinstall = False
        Form1.prkinstall = False
        Form1.payinstall = False
        Form1.adveninstall = False
        Form1.advcampinstall = False
        Form1.advsxinstall = False
        Form1.advdstinstall = False
        Form1.useradmininstall = False
        Form1.sigtoolsinstall = False

        Form1.libctrlsinstall = False
        checkndownload("advance1", "", "http://www.signet.com.gr/PRImages/EditorImages/advance1.zip", Form1.path & "\update\advance1.zip", Form1.advinstall)

        checkndownload("advanceParK", "\Prk\", "http://www.signet.com.gr/PRImages/EditorImages/advanceParK.zip", Form1.path & "\update\advanceParK.zip", Form1.prkinstall)
        checkndownload("advance1en", "\Endyma\", "http://www.signet.com.gr/PRImages/EditorImages/advance1en.zip", Form1.path & "\update\advance1en.zip", Form1.adveninstall)
        checkndownload("advanceDst", "", "http://www.signet.com.gr/PRImages/EditorImages/advanceDst.zip", Form1.path & "\update\advanceDst.zip", Form1.advdstinstall)
        checkndownload("advanceSX", "\Esex\", "http://www.signet.com.gr/PRImages/EditorImages/advanceSX.zip", Form1.path & "\update\advanceSX.zip", Form1.advsxinstall)
        checkndownload("sigtools1", "", "http://www.signet.com.gr/PRImages/EditorImages/sigtools1.zip", Form1.path & "\update\sigtools1.zip", Form1.sigtoolsinstall)
        checkndownload("UserAdministration", "", "", "", Form1.useradmininstall)
        checkndownload("advancecamp", "", "http://www.signet.com.gr/PRImages/EditorImages/advancecamp.zip", Form1.path & "\update\advancecamp.zip", Form1.advcampinstall)

        If CheckedListBox1.CheckedItems.Contains("LibControls") Then
            If System.IO.File.Exists(Form1.path & "\LibControls.ocx") Then
                MessageBox.Show("Το αρχείο LibControls.ocx υπάρχει ήδη. Επιλέξτε Update από την πρώτη φόρμα")
            Else
                Form1.ifexists(Form1.path & "\update\LibControls.zip", Form1.path & "\update\LibControls.ocx")
                Form1.download("http://www.signet.com.gr/PRImages/EditorImages/LibControls.zip", Form1.path & "\update\LibControls.zip")
                Form1.dlcounter = Form1.dlcounter + 1
                Form1.libctrlsinstall = True
            End If
        End If
        If Form1.payrbool = False Then
            checkndownload("AdvancePay", "\Pay\", "http://www.signet.com.gr/PRImages/EditorImages/AdvancePay.zip", Form1.path & "\update\AdvancePay.zip", Form1.payinstall)
        ElseIf Form1.payrbool = True Then
            checkndownload("AdvancePay", "\Payr\", "http://www.signet.com.gr/PRImages/EditorImages/AdvancePay.zip", Form1.path & "\update\AdvancePay.zip", Form1.payinstall)
        End If
        If (Form1.advdstinstall = True Or Form1.adveninstall = True Or Form1.advsxinstall = True Or Form1.advinstall = True Or Form1.advcampinstall = True Or Form1.prkinstall = True Or Form1.payinstall = True Or Form1.sigtoolsinstall = True Or Form1.useradmininstall = True Or Form1.libctrlsinstall = True) Then
            Form1.RadioButton2.Enabled = False
            Form1.completedlds = False
            Form1.Button3.Enabled = False
            Form1.RadioButton1.Enabled = False
            Form1.RadioButton3.Enabled = False
        End If
        Me.Dispose()
        Me.Close()
    End Sub
End Class