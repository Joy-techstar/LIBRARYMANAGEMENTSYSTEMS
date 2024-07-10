Imports System.Data.SqlClient

Public Class Form1
    'variables used to connect to the DB
    Dim sqlconn As SqlConnection
    Dim sqlcmd As SqlCommand
    Dim sqlRd As SqlDataReader
    Dim sqlDt As New DataTable
    Dim DtA As New SqlDataAdapter
    Dim sqlQuery As String

    'my mssql server and database name
    Dim Server As String = "DESKTOP-PO6K48R\MSSQLSERVER01"
    Dim Database As String = "LibraryDB"

    Private bitmap As Bitmap
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'LibraryDBDataSet1.Books1' table. You can move, or remove it, as needed.
        Me.Books1TableAdapter.Fill(Me.LibraryDBDataSet1.Books1)
        'TODO: This line of code loads data into the 'LibraryDBDataSet.Books1' table. You can move, or remove it, as needed.
        Me.Books1TableAdapter.Fill(Me.LibraryDBDataSet1.Books1)
        updatetable()
    End Sub

    Private Sub updatetable()
        Try
            sqlconn = New SqlConnection()
            sqlconn.ConnectionString = $"Server={Server};Database={Database};Integrated Security=True;"

            sqlconn.Open()
            sqlcmd = New SqlCommand("SELECT * FROM Books1", sqlconn)
            sqlRd = sqlcmd.ExecuteReader()

            sqlDt.Clear()
            sqlDt.Load(sqlRd)
            sqlRd.Close()
            sqlconn.Close()
            DataGridView1.DataSource = sqlDt

        Catch ex As Exception
            MessageBox.Show(ex.Message, "MSSQL Connector", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If sqlconn IsNot Nothing AndAlso sqlconn.State = ConnectionState.Open Then
                sqlconn.Close()
            End If
        End Try
    End Sub

    'ADD button functionality
    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            sqlconn = New SqlConnection()
            sqlconn.ConnectionString = $"Server={Server};Database={Database};Integrated Security=True;"

            sqlconn.Open()

            sqlQuery = "INSERT INTO Books1 (Title, Author, YearPublished, Genre) VALUES (@Title, @Author, @YearPublished, @Genre)"

            sqlcmd = New SqlCommand(sqlQuery, sqlconn)
            sqlcmd.Parameters.AddWithValue("@Title", txtTitle.Text)
            sqlcmd.Parameters.AddWithValue("@Author", txtAuthor.Text)
            sqlcmd.Parameters.AddWithValue("@YearPublished", txtYearPublished.Text)
            sqlcmd.Parameters.AddWithValue("@Genre", txtGenre.Text)
            sqlcmd.ExecuteNonQuery()
            sqlconn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "MSSQL Connector", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If sqlconn IsNot Nothing AndAlso sqlconn.State = ConnectionState.Open Then
                sqlconn.Close()
            End If
        End Try

        updatetable()
    End Sub



    ' UPDATE button functionality
    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            sqlconn = New SqlConnection()
            sqlconn.ConnectionString = $"Server={Server};Database={Database};Integrated Security=True;"

            sqlconn.Open()
            'updates the row according to its title
            sqlQuery = "UPDATE Books1 SET Author = @Author, Genre = @Genre, YearPublished = @YearPublished WHERE Title = @Title"

            sqlcmd = New SqlCommand(sqlQuery, sqlconn)
            sqlcmd.Parameters.AddWithValue("@Author", txtAuthor.Text)
            sqlcmd.Parameters.AddWithValue("@Genre", txtGenre.Text)
            sqlcmd.Parameters.AddWithValue("@YearPublished", Integer.Parse(txtYearPublished.Text))
            sqlcmd.Parameters.AddWithValue("@Title", txtTitle.Text)
            sqlcmd.ExecuteNonQuery()
            sqlconn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "MSSQL Connector", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If sqlconn IsNot Nothing AndAlso sqlconn.State = ConnectionState.Open Then
                sqlconn.Close()
            End If
        End Try

        updatetable()
    End Sub
    'DELETE button functionality
    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            sqlconn = New SqlConnection()
            sqlconn.ConnectionString = $"Server={Server};Database={Database};Integrated Security=True;"

            sqlconn.Open()
            'deletes the row wher a certain title exists
            sqlQuery = "DELETE FROM Books1 WHERE Title = @Title"

            sqlcmd = New SqlCommand(sqlQuery, sqlconn)
            sqlcmd.Parameters.AddWithValue("@Author", txtAuthor.Text)
            sqlcmd.Parameters.AddWithValue("@Genre", txtGenre.Text)
            sqlcmd.Parameters.AddWithValue("@YearPublished", Integer.Parse(txtYearPublished.Text))
            sqlcmd.Parameters.AddWithValue("@Title", txtTitle.Text)
            sqlcmd.ExecuteNonQuery()
            sqlconn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "MSSQL Connector", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If sqlconn IsNot Nothing AndAlso sqlconn.State = ConnectionState.Open Then
                sqlconn.Close()
            End If
        End Try

        updatetable()
    End Sub

    'CLEAR button functionality
    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Try
            sqlconn = New SqlConnection()
            sqlconn.ConnectionString = $"Server={Server};Database={Database};Integrated Security=True;"

            sqlconn.Open()
            'deletes all from table books
            sqlQuery = "DELETE FROM Books1"

            sqlcmd = New SqlCommand(sqlQuery, sqlconn)
            sqlcmd.ExecuteNonQuery()
            sqlconn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "MSSQL Connector", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            If sqlconn IsNot Nothing AndAlso sqlconn.State = ConnectionState.Open Then
                sqlconn.Close()
            End If
        End Try

        updatetable()
    End Sub

End Class