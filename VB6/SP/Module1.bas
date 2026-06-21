Attribute VB_Name = "Module1"
Option Explicit

Public Sub CargarPagosManual(sTabla As String)

    Dim rs As Recordset: Set rs = db.OpenRecordset(sTabla)
    Do While Not rs.EOF
        moAdmPrestamo.IngresarPagoParcial rs![Nro#], BuscarFechaPagoPorPrestamo(rs![Nro#]), rs!Importe
        rs.MoveNext
    Loop
    MsgBox "Proceso finalizado", vbExclamation

End Sub

Private Function BuscarFechaPagoPorPrestamo(plIDPrestamo As Long) As Date
    Dim sSql As String
    sSql = "select Min(FechaVencimiento) AS Fecha from sp_factura where IDPrestamo=" & plIDPrestamo & " and fechapago is null"
    Dim rs As Recordset: Set rs = db.OpenRecordset(sSql, dbOpenSnapshot)
    If Not IsNull(rs!Fecha) Then
        BuscarFechaPagoPorPrestamo = rs!Fecha
    Else
        Err.Raise -1, , "Error con la fecha del prestamo & plidprestamo"
    End If
    
End Function
