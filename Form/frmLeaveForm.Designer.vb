<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLeaveForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.txtTotalLeaveCredits = New System.Windows.Forms.Label()
        Me.lblTotalLeaveCredits = New System.Windows.Forms.Label()
        Me.lblFrom = New System.Windows.Forms.Label()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.lblTo = New System.Windows.Forms.Label()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.lblDatesOfLeave = New System.Windows.Forms.Label()
        Me.lblNumberOfDays = New System.Windows.Forms.Label()
        Me.txtNumberOfDays = New System.Windows.Forms.Label()
        Me.lblReason = New System.Windows.Forms.Label()
        Me.txtReason = New System.Windows.Forms.TextBox()
        Me.btnClose = New PinkieControls.ButtonXP()
        Me.btnDelete = New PinkieControls.ButtonXP()
        Me.btnCancel = New PinkieControls.ButtonXP()
        Me.txtBalance = New System.Windows.Forms.Label()
        Me.lblBalance = New System.Windows.Forms.Label()
        Me.lblLeaveType = New System.Windows.Forms.Label()
        Me.cmbLeaveType = New System.Windows.Forms.ComboBox()
        Me.txtDateCreated = New System.Windows.Forms.Label()
        Me.lblDateCreated = New System.Windows.Forms.Label()
        Me.lblRoutingStatus = New System.Windows.Forms.Label()
        Me.txtFileId = New System.Windows.Forms.Label()
        Me.lblFileId = New System.Windows.Forms.Label()
        Me.txtRoutingStatus = New System.Windows.Forms.Label()
        Me.btnSave = New PinkieControls.ButtonXP()
        Me.grpEmpInfo = New System.Windows.Forms.GroupBox()
        Me.txtDateHired = New System.Windows.Forms.Label()
        Me.lbDateHired = New System.Windows.Forms.Label()
        Me.txtIdNumber = New System.Windows.Forms.Label()
        Me.lblIdNumber = New System.Windows.Forms.Label()
        Me.txtEmpStatus = New System.Windows.Forms.Label()
        Me.lblEmpStatus = New System.Windows.Forms.Label()
        Me.lblName = New System.Windows.Forms.Label()
        Me.txtPosition = New System.Windows.Forms.Label()
        Me.lblPosition = New System.Windows.Forms.Label()
        Me.txtDepartment = New System.Windows.Forms.Label()
        Me.lblDepartment = New System.Windows.Forms.Label()
        Me.txtName = New System.Windows.Forms.Label()
        Me.gprLeaveInfo = New System.Windows.Forms.GroupBox()
        Me.lblManagerName = New System.Windows.Forms.Label()
        Me.cmbManagerName = New SergeUtils.EasyCompletionComboBox()
        Me.lblSuperiorName1 = New System.Windows.Forms.Label()
        Me.lblSuperiorName2 = New System.Windows.Forms.Label()
        Me.cmbSuperiorName2 = New SergeUtils.EasyCompletionComboBox()
        Me.cmbSuperiorName1 = New SergeUtils.EasyCompletionComboBox()
        Me.txtClinicName = New System.Windows.Forms.Label()
        Me.lblSuperiorStatus1 = New System.Windows.Forms.Label()
        Me.cmbSuperiorStatus1 = New System.Windows.Forms.ComboBox()
        Me.lblSuperior1 = New System.Windows.Forms.Label()
        Me.lblSuperiorPosition1 = New System.Windows.Forms.Label()
        Me.lblSuperiorDateApproved1 = New System.Windows.Forms.Label()
        Me.lblSuperiorRemarks1 = New System.Windows.Forms.Label()
        Me.txtSuperiorDateApproved1 = New System.Windows.Forms.Label()
        Me.txtSuperiorRemarks1 = New System.Windows.Forms.TextBox()
        Me.txtSuperiorPosition1 = New System.Windows.Forms.Label()
        Me.lblClinic = New System.Windows.Forms.Label()
        Me.txtClinicStatus = New System.Windows.Forms.Label()
        Me.lblClinicName = New System.Windows.Forms.Label()
        Me.lblApprovers = New System.Windows.Forms.Label()
        Me.lblManagerStatus = New System.Windows.Forms.Label()
        Me.cmbManagerStatus = New System.Windows.Forms.ComboBox()
        Me.lblManager = New System.Windows.Forms.Label()
        Me.lblManagerPosition = New System.Windows.Forms.Label()
        Me.lblManagerDateApproved = New System.Windows.Forms.Label()
        Me.lblManagerRemarks = New System.Windows.Forms.Label()
        Me.txtManagerDateApproved = New System.Windows.Forms.Label()
        Me.txtManagerRemarks = New System.Windows.Forms.TextBox()
        Me.txtManagerPosition = New System.Windows.Forms.Label()
        Me.lblSuperiorStatus2 = New System.Windows.Forms.Label()
        Me.cmbSuperiorStatus2 = New System.Windows.Forms.ComboBox()
        Me.lblSuperior2 = New System.Windows.Forms.Label()
        Me.lblSuperiorPosition2 = New System.Windows.Forms.Label()
        Me.lblSuperiorDateApproved2 = New System.Windows.Forms.Label()
        Me.lblSuperiorRemarks2 = New System.Windows.Forms.Label()
        Me.txtSuperiorDateApproved2 = New System.Windows.Forms.Label()
        Me.txtSuperiorRemarks2 = New System.Windows.Forms.TextBox()
        Me.txtSuperiorPosition2 = New System.Windows.Forms.Label()
        Me.lblClinicStatus = New System.Windows.Forms.Label()
        Me.cmbClinicName = New System.Windows.Forms.ComboBox()
        Me.lblClinicPosition = New System.Windows.Forms.Label()
        Me.lblClinicDateApproved = New System.Windows.Forms.Label()
        Me.lblClinicRemarks = New System.Windows.Forms.Label()
        Me.txtClinicDateApproved = New System.Windows.Forms.Label()
        Me.txtClinicRemarks = New System.Windows.Forms.TextBox()
        Me.txtClinicPosition = New System.Windows.Forms.Label()
        Me.grpEmpInfo.SuspendLayout()
        Me.gprLeaveInfo.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtTotalLeaveCredits
        '
        Me.txtTotalLeaveCredits.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.txtTotalLeaveCredits.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtTotalLeaveCredits.Font = New System.Drawing.Font("Verdana", 14.5!)
        Me.txtTotalLeaveCredits.ForeColor = System.Drawing.Color.Black
        Me.txtTotalLeaveCredits.Location = New System.Drawing.Point(829, 20)
        Me.txtTotalLeaveCredits.Name = "txtTotalLeaveCredits"
        Me.txtTotalLeaveCredits.Size = New System.Drawing.Size(93, 26)
        Me.txtTotalLeaveCredits.TabIndex = 304
        Me.txtTotalLeaveCredits.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.txtTotalLeaveCredits.UseCompatibleTextRendering = True
        '
        'lblTotalLeaveCredits
        '
        Me.lblTotalLeaveCredits.BackColor = System.Drawing.SystemColors.Control
        Me.lblTotalLeaveCredits.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalLeaveCredits.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.lblTotalLeaveCredits.ForeColor = System.Drawing.Color.Black
        Me.lblTotalLeaveCredits.Location = New System.Drawing.Point(713, 20)
        Me.lblTotalLeaveCredits.Name = "lblTotalLeaveCredits"
        Me.lblTotalLeaveCredits.Size = New System.Drawing.Size(117, 26)
        Me.lblTotalLeaveCredits.TabIndex = 303
        Me.lblTotalLeaveCredits.Text = " Leave Credits"
        Me.lblTotalLeaveCredits.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblFrom
        '
        Me.lblFrom.BackColor = System.Drawing.SystemColors.Control
        Me.lblFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFrom.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.lblFrom.ForeColor = System.Drawing.Color.Black
        Me.lblFrom.Location = New System.Drawing.Point(153, 48)
        Me.lblFrom.Name = "lblFrom"
        Me.lblFrom.Size = New System.Drawing.Size(100, 26)
        Me.lblFrom.TabIndex = 314
        Me.lblFrom.Text = " Start Date"
        Me.lblFrom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dtpFrom
        '
        Me.dtpFrom.CustomFormat = "MMMM dd,   yyyy"
        Me.dtpFrom.Font = New System.Drawing.Font("Verdana", 11.5!)
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(252, 48)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(280, 26)
        Me.dtpFrom.TabIndex = 1
        '
        'lblTo
        '
        Me.lblTo.BackColor = System.Drawing.SystemColors.Control
        Me.lblTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTo.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.lblTo.ForeColor = System.Drawing.Color.Black
        Me.lblTo.Location = New System.Drawing.Point(536, 48)
        Me.lblTo.Name = "lblTo"
        Me.lblTo.Size = New System.Drawing.Size(100, 26)
        Me.lblTo.TabIndex = 316
        Me.lblTo.Text = " End Date"
        Me.lblTo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'dtpTo
        '
        Me.dtpTo.CustomFormat = "MMMM dd,   yyyy "
        Me.dtpTo.Font = New System.Drawing.Font("Verdana", 11.5!)
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(635, 48)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(287, 26)
        Me.dtpTo.TabIndex = 2
        '
        'lblDatesOfLeave
        '
        Me.lblDatesOfLeave.BackColor = System.Drawing.SystemColors.Control
        Me.lblDatesOfLeave.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDatesOfLeave.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.lblDatesOfLeave.ForeColor = System.Drawing.Color.Black
        Me.lblDatesOfLeave.Location = New System.Drawing.Point(14, 48)
        Me.lblDatesOfLeave.Name = "lblDatesOfLeave"
        Me.lblDatesOfLeave.Size = New System.Drawing.Size(140, 26)
        Me.lblDatesOfLeave.TabIndex = 317
        Me.lblDatesOfLeave.Text = " Date/s of Leave"
        Me.lblDatesOfLeave.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblNumberOfDays
        '
        Me.lblNumberOfDays.BackColor = System.Drawing.SystemColors.Control
        Me.lblNumberOfDays.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblNumberOfDays.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.lblNumberOfDays.ForeColor = System.Drawing.Color.Black
        Me.lblNumberOfDays.Location = New System.Drawing.Point(925, 48)
        Me.lblNumberOfDays.Name = "lblNumberOfDays"
        Me.lblNumberOfDays.Size = New System.Drawing.Size(140, 26)
        Me.lblNumberOfDays.TabIndex = 319
        Me.lblNumberOfDays.Text = " No. of Day(s)"
        Me.lblNumberOfDays.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtNumberOfDays
        '
        Me.txtNumberOfDays.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.txtNumberOfDays.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtNumberOfDays.Font = New System.Drawing.Font("Verdana", 14.5!)
        Me.txtNumberOfDays.ForeColor = System.Drawing.Color.Black
        Me.txtNumberOfDays.Location = New System.Drawing.Point(1064, 48)
        Me.txtNumberOfDays.Name = "txtNumberOfDays"
        Me.txtNumberOfDays.Size = New System.Drawing.Size(100, 26)
        Me.txtNumberOfDays.TabIndex = 320
        Me.txtNumberOfDays.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.txtNumberOfDays.UseCompatibleTextRendering = True
        '
        'lblReason
        '
        Me.lblReason.BackColor = System.Drawing.SystemColors.Control
        Me.lblReason.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblReason.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.lblReason.ForeColor = System.Drawing.Color.Black
        Me.lblReason.Location = New System.Drawing.Point(14, 76)
        Me.lblReason.Name = "lblReason"
        Me.lblReason.Size = New System.Drawing.Size(1150, 26)
        Me.lblReason.TabIndex = 321
        Me.lblReason.Text = " Reason"
        Me.lblReason.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtReason
        '
        Me.txtReason.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtReason.Font = New System.Drawing.Font("Verdana", 11.0!)
        Me.txtReason.Location = New System.Drawing.Point(14, 101)
        Me.txtReason.Multiline = True
        Me.txtReason.Name = "txtReason"
        Me.txtReason.Size = New System.Drawing.Size(1150, 55)
        Me.txtReason.TabIndex = 3
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnClose.DefaultScheme = False
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Verdana", 10.5!)
        Me.btnClose.Hint = "Close leave form"
        Me.btnClose.Location = New System.Drawing.Point(1072, 655)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Scheme = PinkieControls.ButtonXP.Schemes.Blue
        Me.btnClose.Size = New System.Drawing.Size(115, 36)
        Me.btnClose.TabIndex = 328
        Me.btnClose.TabStop = False
        Me.btnClose.Text = " Close"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnDelete.DefaultScheme = False
        Me.btnDelete.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnDelete.Font = New System.Drawing.Font("Verdana", 10.5!)
        Me.btnDelete.Hint = "Delete record"
        Me.btnDelete.Image = Global.LeaveFilingSystem.My.Resources.Resources.Erase_16_x_16
        Me.btnDelete.Location = New System.Drawing.Point(951, 655)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Scheme = PinkieControls.ButtonXP.Schemes.Blue
        Me.btnDelete.Size = New System.Drawing.Size(115, 36)
        Me.btnDelete.TabIndex = 327
        Me.btnDelete.TabStop = False
        Me.btnDelete.Text = "  Delete"
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnCancel.DefaultScheme = False
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Font = New System.Drawing.Font("Verdana", 10.5!)
        Me.btnCancel.Hint = "Cancel changes"
        Me.btnCancel.Location = New System.Drawing.Point(830, 655)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Scheme = PinkieControls.ButtonXP.Schemes.Blue
        Me.btnCancel.Size = New System.Drawing.Size(115, 36)
        Me.btnCancel.TabIndex = 326
        Me.btnCancel.TabStop = False
        Me.btnCancel.Text = " Cancel"
        '
        'txtBalance
        '
        Me.txtBalance.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.txtBalance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtBalance.Font = New System.Drawing.Font("Verdana", 14.5!)
        Me.txtBalance.ForeColor = System.Drawing.Color.Black
        Me.txtBalance.Location = New System.Drawing.Point(1064, 20)
        Me.txtBalance.Name = "txtBalance"
        Me.txtBalance.Size = New System.Drawing.Size(100, 26)
        Me.txtBalance.TabIndex = 359
        Me.txtBalance.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.txtBalance.UseCompatibleTextRendering = True
        '
        'lblBalance
        '
        Me.lblBalance.BackColor = System.Drawing.SystemColors.Control
        Me.lblBalance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblBalance.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.lblBalance.ForeColor = System.Drawing.Color.Black
        Me.lblBalance.Location = New System.Drawing.Point(925, 20)
        Me.lblBalance.Name = "lblBalance"
        Me.lblBalance.Size = New System.Drawing.Size(140, 26)
        Me.lblBalance.TabIndex = 358
        Me.lblBalance.Text = " Balance"
        Me.lblBalance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblLeaveType
        '
        Me.lblLeaveType.BackColor = System.Drawing.SystemColors.Control
        Me.lblLeaveType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblLeaveType.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.lblLeaveType.ForeColor = System.Drawing.Color.Black
        Me.lblLeaveType.Location = New System.Drawing.Point(14, 20)
        Me.lblLeaveType.Name = "lblLeaveType"
        Me.lblLeaveType.Size = New System.Drawing.Size(140, 26)
        Me.lblLeaveType.TabIndex = 357
        Me.lblLeaveType.Text = " Leave Type"
        Me.lblLeaveType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbLeaveType
        '
        Me.cmbLeaveType.DisplayMember = "LeaveTypeName"
        Me.cmbLeaveType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLeaveType.Font = New System.Drawing.Font("Verdana", 12.0!)
        Me.cmbLeaveType.FormattingEnabled = True
        Me.cmbLeaveType.Items.AddRange(New Object() {"Vacation Leave", "Sick Leave"})
        Me.cmbLeaveType.Location = New System.Drawing.Point(153, 20)
        Me.cmbLeaveType.Name = "cmbLeaveType"
        Me.cmbLeaveType.Size = New System.Drawing.Size(557, 26)
        Me.cmbLeaveType.TabIndex = 0
        Me.cmbLeaveType.ValueMember = "Id"
        '
        'txtDateCreated
        '
        Me.txtDateCreated.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDateCreated.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.txtDateCreated.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDateCreated.Font = New System.Drawing.Font("Verdana", 9.5!)
        Me.txtDateCreated.ForeColor = System.Drawing.Color.Black
        Me.txtDateCreated.Location = New System.Drawing.Point(973, 5)
        Me.txtDateCreated.Name = "txtDateCreated"
        Me.txtDateCreated.Size = New System.Drawing.Size(200, 24)
        Me.txtDateCreated.TabIndex = 363
        Me.txtDateCreated.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.txtDateCreated.UseCompatibleTextRendering = True
        '
        'lblDateCreated
        '
        Me.lblDateCreated.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDateCreated.BackColor = System.Drawing.SystemColors.Control
        Me.lblDateCreated.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDateCreated.ForeColor = System.Drawing.Color.Black
        Me.lblDateCreated.Location = New System.Drawing.Point(874, 5)
        Me.lblDateCreated.Name = "lblDateCreated"
        Me.lblDateCreated.Size = New System.Drawing.Size(100, 24)
        Me.lblDateCreated.TabIndex = 362
        Me.lblDateCreated.Text = " Date Created"
        Me.lblDateCreated.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblRoutingStatus
        '
        Me.lblRoutingStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblRoutingStatus.BackColor = System.Drawing.SystemColors.Control
        Me.lblRoutingStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblRoutingStatus.ForeColor = System.Drawing.Color.Black
        Me.lblRoutingStatus.Location = New System.Drawing.Point(471, 5)
        Me.lblRoutingStatus.Name = "lblRoutingStatus"
        Me.lblRoutingStatus.Size = New System.Drawing.Size(100, 24)
        Me.lblRoutingStatus.TabIndex = 368
        Me.lblRoutingStatus.Text = " File Status"
        Me.lblRoutingStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtFileId
        '
        Me.txtFileId.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.txtFileId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtFileId.Font = New System.Drawing.Font("Verdana", 9.5!)
        Me.txtFileId.ForeColor = System.Drawing.Color.Black
        Me.txtFileId.Location = New System.Drawing.Point(78, 5)
        Me.txtFileId.Name = "txtFileId"
        Me.txtFileId.Size = New System.Drawing.Size(100, 24)
        Me.txtFileId.TabIndex = 383
        Me.txtFileId.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.txtFileId.UseCompatibleTextRendering = True
        '
        'lblFileId
        '
        Me.lblFileId.BackColor = System.Drawing.SystemColors.Control
        Me.lblFileId.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblFileId.ForeColor = System.Drawing.Color.Black
        Me.lblFileId.Location = New System.Drawing.Point(9, 5)
        Me.lblFileId.Name = "lblFileId"
        Me.lblFileId.Size = New System.Drawing.Size(70, 24)
        Me.lblFileId.TabIndex = 382
        Me.lblFileId.Text = " File ID"
        Me.lblFileId.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtRoutingStatus
        '
        Me.txtRoutingStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtRoutingStatus.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.txtRoutingStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRoutingStatus.Font = New System.Drawing.Font("Verdana", 9.5!)
        Me.txtRoutingStatus.ForeColor = System.Drawing.Color.Black
        Me.txtRoutingStatus.Location = New System.Drawing.Point(570, 5)
        Me.txtRoutingStatus.Name = "txtRoutingStatus"
        Me.txtRoutingStatus.Size = New System.Drawing.Size(300, 24)
        Me.txtRoutingStatus.TabIndex = 385
        Me.txtRoutingStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.txtRoutingStatus.UseCompatibleTextRendering = True
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnSave.DefaultScheme = False
        Me.btnSave.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnSave.Font = New System.Drawing.Font("Verdana", 10.5!)
        Me.btnSave.Hint = "Save record then close"
        Me.btnSave.Image = Global.LeaveFilingSystem.My.Resources.Resources.Save_16_x_16
        Me.btnSave.Location = New System.Drawing.Point(709, 655)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Scheme = PinkieControls.ButtonXP.Schemes.Blue
        Me.btnSave.Size = New System.Drawing.Size(115, 36)
        Me.btnSave.TabIndex = 325
        Me.btnSave.TabStop = False
        Me.btnSave.Text = "  Save"
        '
        'grpEmpInfo
        '
        Me.grpEmpInfo.Controls.Add(Me.txtDateHired)
        Me.grpEmpInfo.Controls.Add(Me.lbDateHired)
        Me.grpEmpInfo.Controls.Add(Me.txtIdNumber)
        Me.grpEmpInfo.Controls.Add(Me.lblIdNumber)
        Me.grpEmpInfo.Controls.Add(Me.txtEmpStatus)
        Me.grpEmpInfo.Controls.Add(Me.lblEmpStatus)
        Me.grpEmpInfo.Controls.Add(Me.lblName)
        Me.grpEmpInfo.Controls.Add(Me.txtPosition)
        Me.grpEmpInfo.Controls.Add(Me.lblPosition)
        Me.grpEmpInfo.Controls.Add(Me.txtDepartment)
        Me.grpEmpInfo.Controls.Add(Me.lblDepartment)
        Me.grpEmpInfo.Location = New System.Drawing.Point(9, 36)
        Me.grpEmpInfo.Name = "grpEmpInfo"
        Me.grpEmpInfo.Size = New System.Drawing.Size(1178, 106)
        Me.grpEmpInfo.TabIndex = 401
        Me.grpEmpInfo.TabStop = False
        Me.grpEmpInfo.Text = "Employee Information"
        '
        'txtDateHired
        '
        Me.txtDateHired.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.txtDateHired.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.txtDateHired.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDateHired.Font = New System.Drawing.Font("Verdana", 9.5!)
        Me.txtDateHired.ForeColor = System.Drawing.Color.Black
        Me.txtDateHired.Location = New System.Drawing.Point(767, 72)
        Me.txtDateHired.Name = "txtDateHired"
        Me.txtDateHired.Size = New System.Drawing.Size(397, 24)
        Me.txtDateHired.TabIndex = 413
        Me.txtDateHired.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtDateHired.UseCompatibleTextRendering = True
        '
        'lbDateHired
        '
        Me.lbDateHired.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.lbDateHired.BackColor = System.Drawing.SystemColors.Control
        Me.lbDateHired.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lbDateHired.ForeColor = System.Drawing.Color.Black
        Me.lbDateHired.Location = New System.Drawing.Point(629, 72)
        Me.lbDateHired.Name = "lbDateHired"
        Me.lbDateHired.Size = New System.Drawing.Size(140, 24)
        Me.lbDateHired.TabIndex = 412
        Me.lbDateHired.Text = " Date Hired"
        Me.lbDateHired.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtIdNumber
        '
        Me.txtIdNumber.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.txtIdNumber.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.txtIdNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtIdNumber.Font = New System.Drawing.Font("Verdana", 9.5!)
        Me.txtIdNumber.ForeColor = System.Drawing.Color.Black
        Me.txtIdNumber.Location = New System.Drawing.Point(153, 46)
        Me.txtIdNumber.Name = "txtIdNumber"
        Me.txtIdNumber.Size = New System.Drawing.Size(473, 24)
        Me.txtIdNumber.TabIndex = 411
        Me.txtIdNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtIdNumber.UseCompatibleTextRendering = True
        '
        'lblIdNumber
        '
        Me.lblIdNumber.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.lblIdNumber.BackColor = System.Drawing.SystemColors.Control
        Me.lblIdNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblIdNumber.ForeColor = System.Drawing.Color.Black
        Me.lblIdNumber.Location = New System.Drawing.Point(14, 46)
        Me.lblIdNumber.Name = "lblIdNumber"
        Me.lblIdNumber.Size = New System.Drawing.Size(140, 24)
        Me.lblIdNumber.TabIndex = 410
        Me.lblIdNumber.Text = " ID Number"
        Me.lblIdNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtEmpStatus
        '
        Me.txtEmpStatus.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.txtEmpStatus.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.txtEmpStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtEmpStatus.Font = New System.Drawing.Font("Verdana", 9.5!)
        Me.txtEmpStatus.ForeColor = System.Drawing.Color.Black
        Me.txtEmpStatus.Location = New System.Drawing.Point(767, 46)
        Me.txtEmpStatus.Name = "txtEmpStatus"
        Me.txtEmpStatus.Size = New System.Drawing.Size(397, 24)
        Me.txtEmpStatus.TabIndex = 409
        Me.txtEmpStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtEmpStatus.UseCompatibleTextRendering = True
        '
        'lblEmpStatus
        '
        Me.lblEmpStatus.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.lblEmpStatus.BackColor = System.Drawing.SystemColors.Control
        Me.lblEmpStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblEmpStatus.ForeColor = System.Drawing.Color.Black
        Me.lblEmpStatus.Location = New System.Drawing.Point(629, 46)
        Me.lblEmpStatus.Name = "lblEmpStatus"
        Me.lblEmpStatus.Size = New System.Drawing.Size(140, 24)
        Me.lblEmpStatus.TabIndex = 408
        Me.lblEmpStatus.Text = " Emp. Status"
        Me.lblEmpStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblName
        '
        Me.lblName.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.lblName.BackColor = System.Drawing.SystemColors.Control
        Me.lblName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblName.ForeColor = System.Drawing.Color.Black
        Me.lblName.Location = New System.Drawing.Point(14, 20)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(140, 24)
        Me.lblName.TabIndex = 401
        Me.lblName.Text = " Name"
        Me.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtPosition
        '
        Me.txtPosition.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.txtPosition.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.txtPosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPosition.Font = New System.Drawing.Font("Verdana", 9.5!)
        Me.txtPosition.ForeColor = System.Drawing.Color.Black
        Me.txtPosition.Location = New System.Drawing.Point(767, 20)
        Me.txtPosition.Name = "txtPosition"
        Me.txtPosition.Size = New System.Drawing.Size(397, 24)
        Me.txtPosition.TabIndex = 407
        Me.txtPosition.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtPosition.UseCompatibleTextRendering = True
        '
        'lblPosition
        '
        Me.lblPosition.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.lblPosition.BackColor = System.Drawing.SystemColors.Control
        Me.lblPosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPosition.ForeColor = System.Drawing.Color.Black
        Me.lblPosition.Location = New System.Drawing.Point(629, 20)
        Me.lblPosition.Name = "lblPosition"
        Me.lblPosition.Size = New System.Drawing.Size(140, 24)
        Me.lblPosition.TabIndex = 406
        Me.lblPosition.Text = " Position"
        Me.lblPosition.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtDepartment
        '
        Me.txtDepartment.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.txtDepartment.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.txtDepartment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtDepartment.Font = New System.Drawing.Font("Verdana", 9.5!)
        Me.txtDepartment.ForeColor = System.Drawing.Color.Black
        Me.txtDepartment.Location = New System.Drawing.Point(153, 72)
        Me.txtDepartment.Name = "txtDepartment"
        Me.txtDepartment.Size = New System.Drawing.Size(473, 24)
        Me.txtDepartment.TabIndex = 405
        Me.txtDepartment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtDepartment.UseCompatibleTextRendering = True
        '
        'lblDepartment
        '
        Me.lblDepartment.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.lblDepartment.BackColor = System.Drawing.SystemColors.Control
        Me.lblDepartment.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblDepartment.ForeColor = System.Drawing.Color.Black
        Me.lblDepartment.Location = New System.Drawing.Point(14, 72)
        Me.lblDepartment.Name = "lblDepartment"
        Me.lblDepartment.Size = New System.Drawing.Size(140, 24)
        Me.lblDepartment.TabIndex = 404
        Me.lblDepartment.Text = " Department"
        Me.lblDepartment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtName
        '
        Me.txtName.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtName.Font = New System.Drawing.Font("Verdana", 9.5!)
        Me.txtName.ForeColor = System.Drawing.Color.Black
        Me.txtName.Location = New System.Drawing.Point(162, 56)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(473, 24)
        Me.txtName.TabIndex = 402
        Me.txtName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtName.UseCompatibleTextRendering = True
        '
        'gprLeaveInfo
        '
        Me.gprLeaveInfo.Controls.Add(Me.lblManagerName)
        Me.gprLeaveInfo.Controls.Add(Me.cmbManagerName)
        Me.gprLeaveInfo.Controls.Add(Me.lblSuperiorName1)
        Me.gprLeaveInfo.Controls.Add(Me.lblSuperiorName2)
        Me.gprLeaveInfo.Controls.Add(Me.cmbSuperiorName2)
        Me.gprLeaveInfo.Controls.Add(Me.cmbSuperiorName1)
        Me.gprLeaveInfo.Controls.Add(Me.txtClinicName)
        Me.gprLeaveInfo.Controls.Add(Me.lblSuperiorStatus1)
        Me.gprLeaveInfo.Controls.Add(Me.cmbSuperiorStatus1)
        Me.gprLeaveInfo.Controls.Add(Me.lblSuperior1)
        Me.gprLeaveInfo.Controls.Add(Me.lblSuperiorPosition1)
        Me.gprLeaveInfo.Controls.Add(Me.lblSuperiorDateApproved1)
        Me.gprLeaveInfo.Controls.Add(Me.lblSuperiorRemarks1)
        Me.gprLeaveInfo.Controls.Add(Me.txtSuperiorDateApproved1)
        Me.gprLeaveInfo.Controls.Add(Me.txtSuperiorRemarks1)
        Me.gprLeaveInfo.Controls.Add(Me.txtSuperiorPosition1)
        Me.gprLeaveInfo.Controls.Add(Me.lblClinic)
        Me.gprLeaveInfo.Controls.Add(Me.txtClinicStatus)
        Me.gprLeaveInfo.Controls.Add(Me.lblTo)
        Me.gprLeaveInfo.Controls.Add(Me.lblFrom)
        Me.gprLeaveInfo.Controls.Add(Me.lblClinicName)
        Me.gprLeaveInfo.Controls.Add(Me.lblApprovers)
        Me.gprLeaveInfo.Controls.Add(Me.lblManagerStatus)
        Me.gprLeaveInfo.Controls.Add(Me.cmbManagerStatus)
        Me.gprLeaveInfo.Controls.Add(Me.lblManager)
        Me.gprLeaveInfo.Controls.Add(Me.lblManagerPosition)
        Me.gprLeaveInfo.Controls.Add(Me.lblManagerDateApproved)
        Me.gprLeaveInfo.Controls.Add(Me.lblManagerRemarks)
        Me.gprLeaveInfo.Controls.Add(Me.txtManagerDateApproved)
        Me.gprLeaveInfo.Controls.Add(Me.txtManagerRemarks)
        Me.gprLeaveInfo.Controls.Add(Me.txtManagerPosition)
        Me.gprLeaveInfo.Controls.Add(Me.lblSuperiorStatus2)
        Me.gprLeaveInfo.Controls.Add(Me.cmbSuperiorStatus2)
        Me.gprLeaveInfo.Controls.Add(Me.lblSuperior2)
        Me.gprLeaveInfo.Controls.Add(Me.lblSuperiorPosition2)
        Me.gprLeaveInfo.Controls.Add(Me.lblSuperiorDateApproved2)
        Me.gprLeaveInfo.Controls.Add(Me.lblSuperiorRemarks2)
        Me.gprLeaveInfo.Controls.Add(Me.txtSuperiorDateApproved2)
        Me.gprLeaveInfo.Controls.Add(Me.txtSuperiorRemarks2)
        Me.gprLeaveInfo.Controls.Add(Me.txtSuperiorPosition2)
        Me.gprLeaveInfo.Controls.Add(Me.lblClinicStatus)
        Me.gprLeaveInfo.Controls.Add(Me.cmbClinicName)
        Me.gprLeaveInfo.Controls.Add(Me.lblClinicPosition)
        Me.gprLeaveInfo.Controls.Add(Me.lblClinicDateApproved)
        Me.gprLeaveInfo.Controls.Add(Me.lblClinicRemarks)
        Me.gprLeaveInfo.Controls.Add(Me.txtClinicDateApproved)
        Me.gprLeaveInfo.Controls.Add(Me.txtClinicRemarks)
        Me.gprLeaveInfo.Controls.Add(Me.txtClinicPosition)
        Me.gprLeaveInfo.Controls.Add(Me.lblLeaveType)
        Me.gprLeaveInfo.Controls.Add(Me.cmbLeaveType)
        Me.gprLeaveInfo.Controls.Add(Me.txtTotalLeaveCredits)
        Me.gprLeaveInfo.Controls.Add(Me.lblTotalLeaveCredits)
        Me.gprLeaveInfo.Controls.Add(Me.txtBalance)
        Me.gprLeaveInfo.Controls.Add(Me.lblBalance)
        Me.gprLeaveInfo.Controls.Add(Me.dtpTo)
        Me.gprLeaveInfo.Controls.Add(Me.dtpFrom)
        Me.gprLeaveInfo.Controls.Add(Me.lblDatesOfLeave)
        Me.gprLeaveInfo.Controls.Add(Me.lblNumberOfDays)
        Me.gprLeaveInfo.Controls.Add(Me.txtReason)
        Me.gprLeaveInfo.Controls.Add(Me.lblReason)
        Me.gprLeaveInfo.Controls.Add(Me.txtNumberOfDays)
        Me.gprLeaveInfo.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.gprLeaveInfo.Location = New System.Drawing.Point(9, 146)
        Me.gprLeaveInfo.Name = "gprLeaveInfo"
        Me.gprLeaveInfo.Size = New System.Drawing.Size(1178, 503)
        Me.gprLeaveInfo.TabIndex = 402
        Me.gprLeaveInfo.TabStop = False
        Me.gprLeaveInfo.Text = "Fill-up Form"
        '
        'lblManagerName
        '
        Me.lblManagerName.BackColor = System.Drawing.SystemColors.Control
        Me.lblManagerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblManagerName.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.lblManagerName.ForeColor = System.Drawing.Color.Black
        Me.lblManagerName.Location = New System.Drawing.Point(785, 234)
        Me.lblManagerName.Name = "lblManagerName"
        Me.lblManagerName.Size = New System.Drawing.Size(80, 25)
        Me.lblManagerName.TabIndex = 509
        Me.lblManagerName.Text = " Name"
        Me.lblManagerName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbManagerName
        '
        Me.cmbManagerName.DisplayMember = "EmployeeName"
        Me.cmbManagerName.Font = New System.Drawing.Font("Verdana", 10.5!)
        Me.cmbManagerName.FormattingEnabled = True
        Me.cmbManagerName.Location = New System.Drawing.Point(864, 234)
        Me.cmbManagerName.Name = "cmbManagerName"
        Me.cmbManagerName.Size = New System.Drawing.Size(301, 25)
        Me.cmbManagerName.TabIndex = 536
        Me.cmbManagerName.ValueMember = "EmployeeId"
        '
        'lblSuperiorName1
        '
        Me.lblSuperiorName1.BackColor = System.Drawing.SystemColors.Control
        Me.lblSuperiorName1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSuperiorName1.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.lblSuperiorName1.ForeColor = System.Drawing.Color.Black
        Me.lblSuperiorName1.Location = New System.Drawing.Point(14, 234)
        Me.lblSuperiorName1.Name = "lblSuperiorName1"
        Me.lblSuperiorName1.Size = New System.Drawing.Size(80, 25)
        Me.lblSuperiorName1.TabIndex = 522
        Me.lblSuperiorName1.Text = " Name"
        Me.lblSuperiorName1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSuperiorName2
        '
        Me.lblSuperiorName2.BackColor = System.Drawing.SystemColors.Control
        Me.lblSuperiorName2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSuperiorName2.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.lblSuperiorName2.ForeColor = System.Drawing.Color.Black
        Me.lblSuperiorName2.Location = New System.Drawing.Point(400, 234)
        Me.lblSuperiorName2.Name = "lblSuperiorName2"
        Me.lblSuperiorName2.Size = New System.Drawing.Size(80, 25)
        Me.lblSuperiorName2.TabIndex = 498
        Me.lblSuperiorName2.Text = " Name"
        Me.lblSuperiorName2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbSuperiorName2
        '
        Me.cmbSuperiorName2.Font = New System.Drawing.Font("Verdana", 10.5!)
        Me.cmbSuperiorName2.FormattingEnabled = True
        Me.cmbSuperiorName2.Location = New System.Drawing.Point(479, 234)
        Me.cmbSuperiorName2.Name = "cmbSuperiorName2"
        Me.cmbSuperiorName2.Size = New System.Drawing.Size(301, 25)
        Me.cmbSuperiorName2.TabIndex = 535
        '
        'cmbSuperiorName1
        '
        Me.cmbSuperiorName1.Font = New System.Drawing.Font("Verdana", 10.5!)
        Me.cmbSuperiorName1.FormattingEnabled = True
        Me.cmbSuperiorName1.Location = New System.Drawing.Point(93, 234)
        Me.cmbSuperiorName1.Name = "cmbSuperiorName1"
        Me.cmbSuperiorName1.Size = New System.Drawing.Size(301, 25)
        Me.cmbSuperiorName1.TabIndex = 534
        '
        'txtClinicName
        '
        Me.txtClinicName.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.txtClinicName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtClinicName.Font = New System.Drawing.Font("Verdana", 11.0!)
        Me.txtClinicName.ForeColor = System.Drawing.Color.Black
        Me.txtClinicName.Location = New System.Drawing.Point(425, 412)
        Me.txtClinicName.Name = "txtClinicName"
        Me.txtClinicName.Size = New System.Drawing.Size(297, 24)
        Me.txtClinicName.TabIndex = 532
        Me.txtClinicName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtClinicName.UseCompatibleTextRendering = True
        '
        'lblSuperiorStatus1
        '
        Me.lblSuperiorStatus1.BackColor = System.Drawing.SystemColors.Control
        Me.lblSuperiorStatus1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSuperiorStatus1.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.lblSuperiorStatus1.ForeColor = System.Drawing.Color.Black
        Me.lblSuperiorStatus1.Location = New System.Drawing.Point(14, 207)
        Me.lblSuperiorStatus1.Name = "lblSuperiorStatus1"
        Me.lblSuperiorStatus1.Size = New System.Drawing.Size(80, 25)
        Me.lblSuperiorStatus1.TabIndex = 531
        Me.lblSuperiorStatus1.Text = " Status"
        Me.lblSuperiorStatus1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbSuperiorStatus1
        '
        Me.cmbSuperiorStatus1.DisplayMember = "Name"
        Me.cmbSuperiorStatus1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSuperiorStatus1.Font = New System.Drawing.Font("Verdana", 10.5!)
        Me.cmbSuperiorStatus1.FormattingEnabled = True
        Me.cmbSuperiorStatus1.Location = New System.Drawing.Point(93, 207)
        Me.cmbSuperiorStatus1.Name = "cmbSuperiorStatus1"
        Me.cmbSuperiorStatus1.Size = New System.Drawing.Size(301, 25)
        Me.cmbSuperiorStatus1.TabIndex = 530
        Me.cmbSuperiorStatus1.ValueMember = "Id"
        '
        'lblSuperior1
        '
        Me.lblSuperior1.BackColor = System.Drawing.SystemColors.Control
        Me.lblSuperior1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSuperior1.Font = New System.Drawing.Font("Verdana", 11.0!)
        Me.lblSuperior1.ForeColor = System.Drawing.Color.Black
        Me.lblSuperior1.Location = New System.Drawing.Point(14, 181)
        Me.lblSuperior1.Name = "lblSuperior1"
        Me.lblSuperior1.Size = New System.Drawing.Size(380, 24)
        Me.lblSuperior1.TabIndex = 527
        Me.lblSuperior1.Text = "Immediate Superior 1"
        Me.lblSuperior1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSuperiorPosition1
        '
        Me.lblSuperiorPosition1.BackColor = System.Drawing.SystemColors.Control
        Me.lblSuperiorPosition1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSuperiorPosition1.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.lblSuperiorPosition1.ForeColor = System.Drawing.Color.Black
        Me.lblSuperiorPosition1.Location = New System.Drawing.Point(14, 261)
        Me.lblSuperiorPosition1.Name = "lblSuperiorPosition1"
        Me.lblSuperiorPosition1.Size = New System.Drawing.Size(80, 25)
        Me.lblSuperiorPosition1.TabIndex = 523
        Me.lblSuperiorPosition1.Text = " Position"
        Me.lblSuperiorPosition1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSuperiorDateApproved1
        '
        Me.lblSuperiorDateApproved1.BackColor = System.Drawing.SystemColors.Control
        Me.lblSuperiorDateApproved1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSuperiorDateApproved1.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.lblSuperiorDateApproved1.ForeColor = System.Drawing.Color.Black
        Me.lblSuperiorDateApproved1.Location = New System.Drawing.Point(14, 288)
        Me.lblSuperiorDateApproved1.Name = "lblSuperiorDateApproved1"
        Me.lblSuperiorDateApproved1.Size = New System.Drawing.Size(80, 25)
        Me.lblSuperiorDateApproved1.TabIndex = 524
        Me.lblSuperiorDateApproved1.Text = " Date"
        Me.lblSuperiorDateApproved1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSuperiorRemarks1
        '
        Me.lblSuperiorRemarks1.BackColor = System.Drawing.SystemColors.Control
        Me.lblSuperiorRemarks1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSuperiorRemarks1.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.lblSuperiorRemarks1.ForeColor = System.Drawing.Color.Black
        Me.lblSuperiorRemarks1.Location = New System.Drawing.Point(14, 315)
        Me.lblSuperiorRemarks1.Name = "lblSuperiorRemarks1"
        Me.lblSuperiorRemarks1.Size = New System.Drawing.Size(380, 24)
        Me.lblSuperiorRemarks1.TabIndex = 529
        Me.lblSuperiorRemarks1.Text = " Remarks"
        Me.lblSuperiorRemarks1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSuperiorDateApproved1
        '
        Me.txtSuperiorDateApproved1.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.txtSuperiorDateApproved1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSuperiorDateApproved1.Font = New System.Drawing.Font("Verdana", 11.0!)
        Me.txtSuperiorDateApproved1.ForeColor = System.Drawing.Color.Black
        Me.txtSuperiorDateApproved1.Location = New System.Drawing.Point(93, 288)
        Me.txtSuperiorDateApproved1.Name = "txtSuperiorDateApproved1"
        Me.txtSuperiorDateApproved1.Size = New System.Drawing.Size(301, 25)
        Me.txtSuperiorDateApproved1.TabIndex = 525
        Me.txtSuperiorDateApproved1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSuperiorDateApproved1.UseCompatibleTextRendering = True
        '
        'txtSuperiorRemarks1
        '
        Me.txtSuperiorRemarks1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSuperiorRemarks1.Font = New System.Drawing.Font("Verdana", 11.0!)
        Me.txtSuperiorRemarks1.Location = New System.Drawing.Point(14, 338)
        Me.txtSuperiorRemarks1.Multiline = True
        Me.txtSuperiorRemarks1.Name = "txtSuperiorRemarks1"
        Me.txtSuperiorRemarks1.Size = New System.Drawing.Size(380, 50)
        Me.txtSuperiorRemarks1.TabIndex = 528
        '
        'txtSuperiorPosition1
        '
        Me.txtSuperiorPosition1.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.txtSuperiorPosition1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSuperiorPosition1.Font = New System.Drawing.Font("Verdana", 11.0!)
        Me.txtSuperiorPosition1.ForeColor = System.Drawing.Color.Black
        Me.txtSuperiorPosition1.Location = New System.Drawing.Point(93, 261)
        Me.txtSuperiorPosition1.Name = "txtSuperiorPosition1"
        Me.txtSuperiorPosition1.Size = New System.Drawing.Size(301, 25)
        Me.txtSuperiorPosition1.TabIndex = 526
        Me.txtSuperiorPosition1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSuperiorPosition1.UseCompatibleTextRendering = True
        '
        'lblClinic
        '
        Me.lblClinic.AutoSize = True
        Me.lblClinic.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.lblClinic.Location = New System.Drawing.Point(11, 391)
        Me.lblClinic.Name = "lblClinic"
        Me.lblClinic.Size = New System.Drawing.Size(43, 17)
        Me.lblClinic.TabIndex = 520
        Me.lblClinic.Text = "Clinic"
        '
        'txtClinicStatus
        '
        Me.txtClinicStatus.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.txtClinicStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtClinicStatus.Font = New System.Drawing.Font("Verdana", 11.0!)
        Me.txtClinicStatus.ForeColor = System.Drawing.Color.Black
        Me.txtClinicStatus.Location = New System.Drawing.Point(93, 412)
        Me.txtClinicStatus.Name = "txtClinicStatus"
        Me.txtClinicStatus.Size = New System.Drawing.Size(250, 24)
        Me.txtClinicStatus.TabIndex = 519
        Me.txtClinicStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtClinicStatus.UseCompatibleTextRendering = True
        '
        'lblClinicName
        '
        Me.lblClinicName.BackColor = System.Drawing.SystemColors.Control
        Me.lblClinicName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblClinicName.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.lblClinicName.ForeColor = System.Drawing.Color.Black
        Me.lblClinicName.Location = New System.Drawing.Point(346, 412)
        Me.lblClinicName.Name = "lblClinicName"
        Me.lblClinicName.Size = New System.Drawing.Size(80, 24)
        Me.lblClinicName.TabIndex = 487
        Me.lblClinicName.Text = " Name"
        Me.lblClinicName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblApprovers
        '
        Me.lblApprovers.AutoSize = True
        Me.lblApprovers.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.lblApprovers.Location = New System.Drawing.Point(11, 159)
        Me.lblApprovers.Name = "lblApprovers"
        Me.lblApprovers.Size = New System.Drawing.Size(81, 17)
        Me.lblApprovers.TabIndex = 403
        Me.lblApprovers.Text = "Approvers"
        '
        'lblManagerStatus
        '
        Me.lblManagerStatus.BackColor = System.Drawing.SystemColors.Control
        Me.lblManagerStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblManagerStatus.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.lblManagerStatus.ForeColor = System.Drawing.Color.Black
        Me.lblManagerStatus.Location = New System.Drawing.Point(785, 207)
        Me.lblManagerStatus.Name = "lblManagerStatus"
        Me.lblManagerStatus.Size = New System.Drawing.Size(80, 25)
        Me.lblManagerStatus.TabIndex = 518
        Me.lblManagerStatus.Text = " Status"
        Me.lblManagerStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbManagerStatus
        '
        Me.cmbManagerStatus.DisplayMember = "Name"
        Me.cmbManagerStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbManagerStatus.Font = New System.Drawing.Font("Verdana", 10.5!)
        Me.cmbManagerStatus.FormattingEnabled = True
        Me.cmbManagerStatus.Location = New System.Drawing.Point(864, 207)
        Me.cmbManagerStatus.Name = "cmbManagerStatus"
        Me.cmbManagerStatus.Size = New System.Drawing.Size(301, 25)
        Me.cmbManagerStatus.TabIndex = 517
        Me.cmbManagerStatus.ValueMember = "Id"
        '
        'lblManager
        '
        Me.lblManager.BackColor = System.Drawing.SystemColors.Control
        Me.lblManager.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblManager.Font = New System.Drawing.Font("Verdana", 11.0!)
        Me.lblManager.ForeColor = System.Drawing.Color.Black
        Me.lblManager.Location = New System.Drawing.Point(785, 181)
        Me.lblManager.Name = "lblManager"
        Me.lblManager.Size = New System.Drawing.Size(380, 24)
        Me.lblManager.TabIndex = 514
        Me.lblManager.Text = "Manager / Last Approver"
        Me.lblManager.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblManagerPosition
        '
        Me.lblManagerPosition.BackColor = System.Drawing.SystemColors.Control
        Me.lblManagerPosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblManagerPosition.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.lblManagerPosition.ForeColor = System.Drawing.Color.Black
        Me.lblManagerPosition.Location = New System.Drawing.Point(785, 261)
        Me.lblManagerPosition.Name = "lblManagerPosition"
        Me.lblManagerPosition.Size = New System.Drawing.Size(80, 25)
        Me.lblManagerPosition.TabIndex = 510
        Me.lblManagerPosition.Text = " Position"
        Me.lblManagerPosition.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblManagerDateApproved
        '
        Me.lblManagerDateApproved.BackColor = System.Drawing.SystemColors.Control
        Me.lblManagerDateApproved.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblManagerDateApproved.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.lblManagerDateApproved.ForeColor = System.Drawing.Color.Black
        Me.lblManagerDateApproved.Location = New System.Drawing.Point(785, 288)
        Me.lblManagerDateApproved.Name = "lblManagerDateApproved"
        Me.lblManagerDateApproved.Size = New System.Drawing.Size(80, 25)
        Me.lblManagerDateApproved.TabIndex = 511
        Me.lblManagerDateApproved.Text = " Date"
        Me.lblManagerDateApproved.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblManagerRemarks
        '
        Me.lblManagerRemarks.BackColor = System.Drawing.SystemColors.Control
        Me.lblManagerRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblManagerRemarks.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.lblManagerRemarks.ForeColor = System.Drawing.Color.Black
        Me.lblManagerRemarks.Location = New System.Drawing.Point(785, 315)
        Me.lblManagerRemarks.Name = "lblManagerRemarks"
        Me.lblManagerRemarks.Size = New System.Drawing.Size(380, 24)
        Me.lblManagerRemarks.TabIndex = 516
        Me.lblManagerRemarks.Text = " Remarks"
        Me.lblManagerRemarks.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtManagerDateApproved
        '
        Me.txtManagerDateApproved.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.txtManagerDateApproved.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtManagerDateApproved.Font = New System.Drawing.Font("Verdana", 11.0!)
        Me.txtManagerDateApproved.ForeColor = System.Drawing.Color.Black
        Me.txtManagerDateApproved.Location = New System.Drawing.Point(864, 288)
        Me.txtManagerDateApproved.Name = "txtManagerDateApproved"
        Me.txtManagerDateApproved.Size = New System.Drawing.Size(301, 25)
        Me.txtManagerDateApproved.TabIndex = 512
        Me.txtManagerDateApproved.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtManagerDateApproved.UseCompatibleTextRendering = True
        '
        'txtManagerRemarks
        '
        Me.txtManagerRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtManagerRemarks.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.txtManagerRemarks.Location = New System.Drawing.Point(785, 338)
        Me.txtManagerRemarks.Multiline = True
        Me.txtManagerRemarks.Name = "txtManagerRemarks"
        Me.txtManagerRemarks.Size = New System.Drawing.Size(380, 50)
        Me.txtManagerRemarks.TabIndex = 515
        '
        'txtManagerPosition
        '
        Me.txtManagerPosition.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.txtManagerPosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtManagerPosition.Font = New System.Drawing.Font("Verdana", 11.0!)
        Me.txtManagerPosition.ForeColor = System.Drawing.Color.Black
        Me.txtManagerPosition.Location = New System.Drawing.Point(864, 261)
        Me.txtManagerPosition.Name = "txtManagerPosition"
        Me.txtManagerPosition.Size = New System.Drawing.Size(301, 25)
        Me.txtManagerPosition.TabIndex = 513
        Me.txtManagerPosition.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtManagerPosition.UseCompatibleTextRendering = True
        '
        'lblSuperiorStatus2
        '
        Me.lblSuperiorStatus2.BackColor = System.Drawing.SystemColors.Control
        Me.lblSuperiorStatus2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSuperiorStatus2.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.lblSuperiorStatus2.ForeColor = System.Drawing.Color.Black
        Me.lblSuperiorStatus2.Location = New System.Drawing.Point(400, 207)
        Me.lblSuperiorStatus2.Name = "lblSuperiorStatus2"
        Me.lblSuperiorStatus2.Size = New System.Drawing.Size(80, 25)
        Me.lblSuperiorStatus2.TabIndex = 507
        Me.lblSuperiorStatus2.Text = " Status"
        Me.lblSuperiorStatus2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbSuperiorStatus2
        '
        Me.cmbSuperiorStatus2.DisplayMember = "Name"
        Me.cmbSuperiorStatus2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSuperiorStatus2.Font = New System.Drawing.Font("Verdana", 10.5!)
        Me.cmbSuperiorStatus2.FormattingEnabled = True
        Me.cmbSuperiorStatus2.Location = New System.Drawing.Point(479, 207)
        Me.cmbSuperiorStatus2.Name = "cmbSuperiorStatus2"
        Me.cmbSuperiorStatus2.Size = New System.Drawing.Size(301, 25)
        Me.cmbSuperiorStatus2.TabIndex = 506
        Me.cmbSuperiorStatus2.ValueMember = "Id"
        '
        'lblSuperior2
        '
        Me.lblSuperior2.BackColor = System.Drawing.SystemColors.Control
        Me.lblSuperior2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSuperior2.Font = New System.Drawing.Font("Verdana", 11.0!)
        Me.lblSuperior2.ForeColor = System.Drawing.Color.Black
        Me.lblSuperior2.Location = New System.Drawing.Point(400, 181)
        Me.lblSuperior2.Name = "lblSuperior2"
        Me.lblSuperior2.Size = New System.Drawing.Size(380, 24)
        Me.lblSuperior2.TabIndex = 503
        Me.lblSuperior2.Text = "Immediate Superior 2"
        Me.lblSuperior2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblSuperiorPosition2
        '
        Me.lblSuperiorPosition2.BackColor = System.Drawing.SystemColors.Control
        Me.lblSuperiorPosition2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSuperiorPosition2.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.lblSuperiorPosition2.ForeColor = System.Drawing.Color.Black
        Me.lblSuperiorPosition2.Location = New System.Drawing.Point(400, 261)
        Me.lblSuperiorPosition2.Name = "lblSuperiorPosition2"
        Me.lblSuperiorPosition2.Size = New System.Drawing.Size(80, 25)
        Me.lblSuperiorPosition2.TabIndex = 499
        Me.lblSuperiorPosition2.Text = " Position"
        Me.lblSuperiorPosition2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSuperiorDateApproved2
        '
        Me.lblSuperiorDateApproved2.BackColor = System.Drawing.SystemColors.Control
        Me.lblSuperiorDateApproved2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSuperiorDateApproved2.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.lblSuperiorDateApproved2.ForeColor = System.Drawing.Color.Black
        Me.lblSuperiorDateApproved2.Location = New System.Drawing.Point(400, 288)
        Me.lblSuperiorDateApproved2.Name = "lblSuperiorDateApproved2"
        Me.lblSuperiorDateApproved2.Size = New System.Drawing.Size(80, 25)
        Me.lblSuperiorDateApproved2.TabIndex = 500
        Me.lblSuperiorDateApproved2.Text = " Date"
        Me.lblSuperiorDateApproved2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSuperiorRemarks2
        '
        Me.lblSuperiorRemarks2.BackColor = System.Drawing.SystemColors.Control
        Me.lblSuperiorRemarks2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSuperiorRemarks2.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.lblSuperiorRemarks2.ForeColor = System.Drawing.Color.Black
        Me.lblSuperiorRemarks2.Location = New System.Drawing.Point(400, 315)
        Me.lblSuperiorRemarks2.Name = "lblSuperiorRemarks2"
        Me.lblSuperiorRemarks2.Size = New System.Drawing.Size(380, 24)
        Me.lblSuperiorRemarks2.TabIndex = 505
        Me.lblSuperiorRemarks2.Text = " Remarks"
        Me.lblSuperiorRemarks2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSuperiorDateApproved2
        '
        Me.txtSuperiorDateApproved2.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.txtSuperiorDateApproved2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSuperiorDateApproved2.Font = New System.Drawing.Font("Verdana", 11.0!)
        Me.txtSuperiorDateApproved2.ForeColor = System.Drawing.Color.Black
        Me.txtSuperiorDateApproved2.Location = New System.Drawing.Point(479, 288)
        Me.txtSuperiorDateApproved2.Name = "txtSuperiorDateApproved2"
        Me.txtSuperiorDateApproved2.Size = New System.Drawing.Size(301, 25)
        Me.txtSuperiorDateApproved2.TabIndex = 501
        Me.txtSuperiorDateApproved2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSuperiorDateApproved2.UseCompatibleTextRendering = True
        '
        'txtSuperiorRemarks2
        '
        Me.txtSuperiorRemarks2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSuperiorRemarks2.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.txtSuperiorRemarks2.Location = New System.Drawing.Point(400, 338)
        Me.txtSuperiorRemarks2.Multiline = True
        Me.txtSuperiorRemarks2.Name = "txtSuperiorRemarks2"
        Me.txtSuperiorRemarks2.Size = New System.Drawing.Size(380, 50)
        Me.txtSuperiorRemarks2.TabIndex = 504
        '
        'txtSuperiorPosition2
        '
        Me.txtSuperiorPosition2.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.txtSuperiorPosition2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSuperiorPosition2.Font = New System.Drawing.Font("Verdana", 11.0!)
        Me.txtSuperiorPosition2.ForeColor = System.Drawing.Color.Black
        Me.txtSuperiorPosition2.Location = New System.Drawing.Point(479, 261)
        Me.txtSuperiorPosition2.Name = "txtSuperiorPosition2"
        Me.txtSuperiorPosition2.Size = New System.Drawing.Size(301, 25)
        Me.txtSuperiorPosition2.TabIndex = 502
        Me.txtSuperiorPosition2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtSuperiorPosition2.UseCompatibleTextRendering = True
        '
        'lblClinicStatus
        '
        Me.lblClinicStatus.BackColor = System.Drawing.SystemColors.Control
        Me.lblClinicStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblClinicStatus.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.lblClinicStatus.ForeColor = System.Drawing.Color.Black
        Me.lblClinicStatus.Location = New System.Drawing.Point(14, 412)
        Me.lblClinicStatus.Name = "lblClinicStatus"
        Me.lblClinicStatus.Size = New System.Drawing.Size(80, 24)
        Me.lblClinicStatus.TabIndex = 496
        Me.lblClinicStatus.Text = " Status"
        Me.lblClinicStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cmbClinicName
        '
        Me.cmbClinicName.DisplayMember = "Name"
        Me.cmbClinicName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbClinicName.Enabled = False
        Me.cmbClinicName.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.cmbClinicName.FormattingEnabled = True
        Me.cmbClinicName.Location = New System.Drawing.Point(425, 412)
        Me.cmbClinicName.Name = "cmbClinicName"
        Me.cmbClinicName.Size = New System.Drawing.Size(297, 24)
        Me.cmbClinicName.TabIndex = 486
        Me.cmbClinicName.ValueMember = "Id"
        '
        'lblClinicPosition
        '
        Me.lblClinicPosition.BackColor = System.Drawing.SystemColors.Control
        Me.lblClinicPosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblClinicPosition.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.lblClinicPosition.ForeColor = System.Drawing.Color.Black
        Me.lblClinicPosition.Location = New System.Drawing.Point(346, 438)
        Me.lblClinicPosition.Name = "lblClinicPosition"
        Me.lblClinicPosition.Size = New System.Drawing.Size(80, 24)
        Me.lblClinicPosition.TabIndex = 488
        Me.lblClinicPosition.Text = " Position"
        Me.lblClinicPosition.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblClinicDateApproved
        '
        Me.lblClinicDateApproved.BackColor = System.Drawing.SystemColors.Control
        Me.lblClinicDateApproved.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblClinicDateApproved.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.lblClinicDateApproved.ForeColor = System.Drawing.Color.Black
        Me.lblClinicDateApproved.Location = New System.Drawing.Point(14, 438)
        Me.lblClinicDateApproved.Name = "lblClinicDateApproved"
        Me.lblClinicDateApproved.Size = New System.Drawing.Size(80, 24)
        Me.lblClinicDateApproved.TabIndex = 489
        Me.lblClinicDateApproved.Text = " Date"
        Me.lblClinicDateApproved.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblClinicRemarks
        '
        Me.lblClinicRemarks.BackColor = System.Drawing.SystemColors.Control
        Me.lblClinicRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblClinicRemarks.Font = New System.Drawing.Font("Verdana", 10.0!)
        Me.lblClinicRemarks.ForeColor = System.Drawing.Color.Black
        Me.lblClinicRemarks.Location = New System.Drawing.Point(725, 411)
        Me.lblClinicRemarks.Name = "lblClinicRemarks"
        Me.lblClinicRemarks.Size = New System.Drawing.Size(440, 24)
        Me.lblClinicRemarks.TabIndex = 494
        Me.lblClinicRemarks.Text = " Remarks"
        Me.lblClinicRemarks.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtClinicDateApproved
        '
        Me.txtClinicDateApproved.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.txtClinicDateApproved.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtClinicDateApproved.Font = New System.Drawing.Font("Verdana", 11.0!)
        Me.txtClinicDateApproved.ForeColor = System.Drawing.Color.Black
        Me.txtClinicDateApproved.Location = New System.Drawing.Point(93, 438)
        Me.txtClinicDateApproved.Name = "txtClinicDateApproved"
        Me.txtClinicDateApproved.Size = New System.Drawing.Size(250, 24)
        Me.txtClinicDateApproved.TabIndex = 490
        Me.txtClinicDateApproved.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtClinicDateApproved.UseCompatibleTextRendering = True
        '
        'txtClinicRemarks
        '
        Me.txtClinicRemarks.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtClinicRemarks.Font = New System.Drawing.Font("Verdana", 11.0!)
        Me.txtClinicRemarks.Location = New System.Drawing.Point(725, 434)
        Me.txtClinicRemarks.Multiline = True
        Me.txtClinicRemarks.Name = "txtClinicRemarks"
        Me.txtClinicRemarks.ReadOnly = True
        Me.txtClinicRemarks.Size = New System.Drawing.Size(440, 60)
        Me.txtClinicRemarks.TabIndex = 493
        '
        'txtClinicPosition
        '
        Me.txtClinicPosition.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.txtClinicPosition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtClinicPosition.Font = New System.Drawing.Font("Verdana", 11.0!)
        Me.txtClinicPosition.ForeColor = System.Drawing.Color.Black
        Me.txtClinicPosition.Location = New System.Drawing.Point(425, 438)
        Me.txtClinicPosition.Name = "txtClinicPosition"
        Me.txtClinicPosition.Size = New System.Drawing.Size(297, 24)
        Me.txtClinicPosition.TabIndex = 491
        Me.txtClinicPosition.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.txtClinicPosition.UseCompatibleTextRendering = True
        '
        'frmLeaveForm
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(1196, 697)
        Me.Controls.Add(Me.gprLeaveInfo)
        Me.Controls.Add(Me.lblDateCreated)
        Me.Controls.Add(Me.txtDateCreated)
        Me.Controls.Add(Me.lblRoutingStatus)
        Me.Controls.Add(Me.lblFileId)
        Me.Controls.Add(Me.txtFileId)
        Me.Controls.Add(Me.txtRoutingStatus)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.grpEmpInfo)
        Me.Font = New System.Drawing.Font("Verdana", 8.5!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLeaveForm"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Leave Entry Form"
        Me.grpEmpInfo.ResumeLayout(False)
        Me.gprLeaveInfo.ResumeLayout(False)
        Me.gprLeaveInfo.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtTotalLeaveCredits As System.Windows.Forms.Label
    Friend WithEvents lblTotalLeaveCredits As System.Windows.Forms.Label
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Public WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblTo As System.Windows.Forms.Label
    Public WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblDatesOfLeave As System.Windows.Forms.Label
    Friend WithEvents lblNumberOfDays As System.Windows.Forms.Label
    Friend WithEvents txtNumberOfDays As System.Windows.Forms.Label
    Friend WithEvents lblReason As System.Windows.Forms.Label
    Friend WithEvents txtReason As System.Windows.Forms.TextBox
    Friend WithEvents btnClose As PinkieControls.ButtonXP
    Friend WithEvents btnDelete As PinkieControls.ButtonXP
    Friend WithEvents btnCancel As PinkieControls.ButtonXP
    Friend WithEvents txtBalance As System.Windows.Forms.Label
    Friend WithEvents lblBalance As System.Windows.Forms.Label
    Friend WithEvents lblLeaveType As System.Windows.Forms.Label
    Friend WithEvents cmbLeaveType As System.Windows.Forms.ComboBox
    Friend WithEvents txtDateCreated As System.Windows.Forms.Label
    Friend WithEvents lblDateCreated As System.Windows.Forms.Label
    Friend WithEvents lblRoutingStatus As System.Windows.Forms.Label
    Friend WithEvents txtFileId As System.Windows.Forms.Label
    Friend WithEvents lblFileId As System.Windows.Forms.Label
    Friend WithEvents txtRoutingStatus As System.Windows.Forms.Label
    Friend WithEvents btnSave As PinkieControls.ButtonXP
    Friend WithEvents grpEmpInfo As System.Windows.Forms.GroupBox
    Friend WithEvents txtDateHired As System.Windows.Forms.Label
    Friend WithEvents lbDateHired As System.Windows.Forms.Label
    Friend WithEvents txtIdNumber As System.Windows.Forms.Label
    Friend WithEvents lblIdNumber As System.Windows.Forms.Label
    Friend WithEvents txtEmpStatus As System.Windows.Forms.Label
    Friend WithEvents lblEmpStatus As System.Windows.Forms.Label
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents txtPosition As System.Windows.Forms.Label
    Friend WithEvents txtName As System.Windows.Forms.Label
    Friend WithEvents lblPosition As System.Windows.Forms.Label
    Friend WithEvents txtDepartment As System.Windows.Forms.Label
    Friend WithEvents lblDepartment As System.Windows.Forms.Label
    Friend WithEvents gprLeaveInfo As System.Windows.Forms.GroupBox
    Friend WithEvents lblManagerStatus As System.Windows.Forms.Label
    Friend WithEvents cmbManagerStatus As System.Windows.Forms.ComboBox
    Friend WithEvents lblManager As System.Windows.Forms.Label
    Friend WithEvents lblManagerName As System.Windows.Forms.Label
    Friend WithEvents lblManagerPosition As System.Windows.Forms.Label
    Friend WithEvents lblManagerDateApproved As System.Windows.Forms.Label
    Friend WithEvents lblManagerRemarks As System.Windows.Forms.Label
    Friend WithEvents txtManagerDateApproved As System.Windows.Forms.Label
    Friend WithEvents txtManagerRemarks As System.Windows.Forms.TextBox
    Friend WithEvents txtManagerPosition As System.Windows.Forms.Label
    Friend WithEvents lblSuperiorStatus2 As System.Windows.Forms.Label
    Friend WithEvents cmbSuperiorStatus2 As System.Windows.Forms.ComboBox
    Friend WithEvents lblSuperior2 As System.Windows.Forms.Label
    Friend WithEvents lblSuperiorName2 As System.Windows.Forms.Label
    Friend WithEvents lblSuperiorPosition2 As System.Windows.Forms.Label
    Friend WithEvents lblSuperiorDateApproved2 As System.Windows.Forms.Label
    Friend WithEvents lblSuperiorRemarks2 As System.Windows.Forms.Label
    Friend WithEvents txtSuperiorDateApproved2 As System.Windows.Forms.Label
    Friend WithEvents txtSuperiorRemarks2 As System.Windows.Forms.TextBox
    Friend WithEvents txtSuperiorPosition2 As System.Windows.Forms.Label
    Friend WithEvents lblApprovers As System.Windows.Forms.Label
    Friend WithEvents lblClinicName As System.Windows.Forms.Label
    Friend WithEvents lblClinicStatus As System.Windows.Forms.Label
    Friend WithEvents cmbClinicName As System.Windows.Forms.ComboBox
    Friend WithEvents lblClinicPosition As System.Windows.Forms.Label
    Friend WithEvents lblClinicDateApproved As System.Windows.Forms.Label
    Friend WithEvents lblClinicRemarks As System.Windows.Forms.Label
    Friend WithEvents txtClinicDateApproved As System.Windows.Forms.Label
    Friend WithEvents txtClinicRemarks As System.Windows.Forms.TextBox
    Friend WithEvents txtClinicPosition As System.Windows.Forms.Label
    Friend WithEvents txtClinicStatus As System.Windows.Forms.Label
    Friend WithEvents lblClinic As System.Windows.Forms.Label
    Friend WithEvents lblSuperiorName1 As System.Windows.Forms.Label
    Friend WithEvents lblSuperiorStatus1 As System.Windows.Forms.Label
    Friend WithEvents cmbSuperiorStatus1 As System.Windows.Forms.ComboBox
    Friend WithEvents lblSuperior1 As System.Windows.Forms.Label
    Friend WithEvents lblSuperiorPosition1 As System.Windows.Forms.Label
    Friend WithEvents lblSuperiorDateApproved1 As System.Windows.Forms.Label
    Friend WithEvents lblSuperiorRemarks1 As System.Windows.Forms.Label
    Friend WithEvents txtSuperiorDateApproved1 As System.Windows.Forms.Label
    Friend WithEvents txtSuperiorRemarks1 As System.Windows.Forms.TextBox
    Friend WithEvents txtSuperiorPosition1 As System.Windows.Forms.Label
    Friend WithEvents txtClinicName As System.Windows.Forms.Label
    Friend WithEvents cmbSuperiorName1 As SergeUtils.EasyCompletionComboBox
    Friend WithEvents cmbSuperiorName2 As SergeUtils.EasyCompletionComboBox
    Friend WithEvents cmbManagerName As SergeUtils.EasyCompletionComboBox
End Class
