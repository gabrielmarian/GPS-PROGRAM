Imports System.Text
Imports CefSharp.WinForms
Imports CefSharp

Public Class Gps

    Private WithEvents browser As ChromiumWebBrowser
    Private WithEvents wiki As ChromiumWebBrowser

    Public Sub New()
        InitializeComponent()

        Dim settings As New CefSettings()
        CefSharp.Cef.Initialize(settings)

        browser = New ChromiumWebBrowser("http://maps.google.com/maps?q=") With {
            .Dock = DockStyle.Fill
        }
        Panel3.Controls.Add(browser)

        wiki = New ChromiumWebBrowser("https://ro.wikipedia.org/wiki/") With {
            .Dock = DockStyle.Fill
        }
        Panel4.Controls.Add(wiki)

    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'Access_databaseDataSet.Table1' table. You can move, or remove it, as needed.
        Me.Table1TableAdapter.Fill(Me.Access_databaseDataSet.Table1)
        'TODO: This line of code loads data into the 'Access_databaseDataSet.Table2' table. You can move, or remove it, as needed.
        Me.Table2TableAdapter.Fill(Me.Access_databaseDataSet.Table2)
        WebBrowser2.Navigate("http://iplocation.com")
        Timer1.Start()

    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Stop()

        Dim arreyTag As New ArrayList

        For Each items As HtmlElement In WebBrowser2.Document.GetElementsByTagName("td")
            arreyTag.Add(items.InnerText)
        Next

        lblIP.Text = arreyTag(0)
        lblLatitude.Text = arreyTag(1)
        lblLongitude.Text = arreyTag(2)
        lblCountry.Text = arreyTag(3)
        lblRegion.Text = arreyTag(4)
        lblCity.Text = arreyTag(5)
        lblProvider.Text = arreyTag(6)

        btnLatitude.PerformClick()

    End Sub

    Private Sub btnLatitude_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLatitude.Click
        Try
            browser.Load("http://maps.google.com/maps?q=" + lblLatitude.Text + " %2C " + lblLongitude.Text)
        Catch ex As Exception
            MsgBox(ex.Message, "Error")
        End Try

    End Sub

    Private Sub btnCity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCity.Click
        wiki.Load("https://ro.wikipedia.org/wiki/" + lblCity.Text)
        Try
            browser.Load("http://maps.google.com/maps?q=" + lblCity.Text)
        Catch ex As Exception
            MsgBox(ex.Message, "Error")
        End Try

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            browser.Load("http://maps.google.com/maps?q=" + TextBox_choose_a_city.Text + " " + TextBox_sfto.Text)
        Catch ex As Exception
            MessageBox.Show("Unable to retrive date")
        End Try

    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        Dim arreyTag As New ArrayList

        For Each items As HtmlElement In WebBrowser2.Document.GetElementsByTagName("td")
            arreyTag.Add(items.InnerText)
        Next

        lblIP.Text = arreyTag(0)
        lblLatitude.Text = arreyTag(1)
        lblLongitude.Text = arreyTag(2)
        lblCountry.Text = arreyTag(3)
        lblRegion.Text = arreyTag(4)
        lblCity.Text = arreyTag(5)
        lblProvider.Text = arreyTag(6)

    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Table2BindingSource.AddNew()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Table1BindingSource.AddNew()
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        On Error GoTo SaveErr
        Table2BindingSource.EndEdit()
        Table2TableAdapter.Update(Access_databaseDataSet.Table2)
        MessageBox.Show("Saved coordinates!")
SaveErr:
        Exit Sub
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        On Error GoTo SaveErr
        Table1BindingSource.EndEdit()
        Table1TableAdapter.Update(Access_databaseDataSet.Table1)
        MessageBox.Show("Save!")
SaveErr:
        Exit Sub
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Table2BindingSource.MovePrevious()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Table1BindingSource.MovePrevious()
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Table2BindingSource.MoveNext()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Table1BindingSource.MoveNext()
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Table2BindingSource.RemoveCurrent()
    End Sub

    Private Sub delete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles delete.Click
        Table1BindingSource.RemoveCurrent()
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        Timer2.Start()
        If Not Panel1.Height = 0 Then
            Panel1.Height = 0
            Timer2.Enabled = False
        End If
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Panel1.Height = 120
        Timer2.Stop()
    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
        Timer3.Start()
        If Not Panel2.Height = 0 Then
            Panel2.Height = 0
            Timer3.Enabled = False
        End If
    End Sub

    Private Sub Timer3_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer3.Tick
        Panel2.Height = 120
        Timer3.Stop()
    End Sub

    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click
        wiki.Load("https://ro.wikipedia.org/wiki/" + TextBox_choose_a_city.Text)

        Timer4.Start()
        If Not Panel4.Height = 0 Then
            Panel4.Height = 0
            Timer4.Enabled = False
        End If
    End Sub

    Private Sub Timer4_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer4.Tick
        Panel4.Height = 750
        Timer4.Stop()
    End Sub
End Class
