Imports System.Configuration
Imports System.Data.SqlClient

Public Class clsConnection
    Private con As SqlConnection

    Public Function LocalConnection() As String
        Return ConfigurationManager.ConnectionStrings("LeaveFilingSystem.My.MySettings.LeaveFilingConnectionString").ConnectionString
    End Function

    Public Function JeonsoftConnection() As String
        Return ConfigurationManager.ConnectionStrings("LeaveFilingSystem.My.MySettings.NBCTECHDBConnectionString").ConnectionString
    End Function

End Class