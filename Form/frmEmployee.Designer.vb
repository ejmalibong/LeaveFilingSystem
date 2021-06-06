<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEmployee
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEmployee))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.bindingNavigator = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.txtTotalPageNumber = New System.Windows.Forms.ToolStripLabel()
        Me.BindingNavigatorMoveFirstItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMovePreviousItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.txtPageNumber = New System.Windows.Forms.ToolStripTextBox()
        Me.BindingNavigatorSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorMoveNextItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMoveLastItem = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.btnGo = New System.Windows.Forms.ToolStripButton()
        Me.btnSync = New PinkieControls.ButtonXP()
        Me.btnSave = New PinkieControls.ButtonXP()
        Me.btnClose = New PinkieControls.ButtonXP()
        Me.btnDelete = New PinkieControls.ButtonXP()
        Me.btnCancel = New PinkieControls.ButtonXP()
        Me.btnAdd = New PinkieControls.ButtonXP()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.pnlDate = New System.Windows.Forms.Panel()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.btnReset = New PinkieControls.ButtonXP()
        Me.btnSearch = New PinkieControls.ButtonXP()
        Me.lblSearchCriteria = New System.Windows.Forms.Label()
        Me.cmbSearchCriteria = New System.Windows.Forms.ComboBox()
        Me.dgvList = New System.Windows.Forms.DataGridView()
        Me.ColEmployeeId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColEmployeeCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColEmployeeName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColPassword = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColNbcEmailAddress = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColIsApprover = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.ColIsHrRecords = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.ColIsEmployee = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.ColIsHoliday = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.ColIsActive = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        CType(Me.bindingNavigator, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.bindingNavigator.SuspendLayout()
        Me.pnlTop.SuspendLayout()
        Me.pnlDate.SuspendLayout()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'bindingNavigator
        '
        Me.bindingNavigator.AddNewItem = Nothing
        Me.bindingNavigator.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.bindingNavigator.BackColor = System.Drawing.Color.White
        Me.bindingNavigator.CountItem = Me.txtTotalPageNumber
        Me.bindingNavigator.CountItemFormat = "of "
        Me.bindingNavigator.DeleteItem = Nothing
        Me.bindingNavigator.Dock = System.Windows.Forms.DockStyle.None
        Me.bindingNavigator.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.bindingNavigator.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.bindingNavigator.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BindingNavigatorMoveFirstItem, Me.BindingNavigatorMovePreviousItem, Me.BindingNavigatorSeparator, Me.txtPageNumber, Me.txtTotalPageNumber, Me.BindingNavigatorSeparator1, Me.BindingNavigatorMoveNextItem, Me.BindingNavigatorMoveLastItem, Me.ToolStripSeparator, Me.btnGo})
        Me.bindingNavigator.Location = New System.Drawing.Point(3, 524)
        Me.bindingNavigator.MoveFirstItem = Me.BindingNavigatorMoveFirstItem
        Me.bindingNavigator.MoveLastItem = Me.BindingNavigatorMoveLastItem
        Me.bindingNavigator.MoveNextItem = Me.BindingNavigatorMoveNextItem
        Me.bindingNavigator.MovePreviousItem = Me.BindingNavigatorMovePreviousItem
        Me.bindingNavigator.Name = "bindingNavigator"
        Me.bindingNavigator.PositionItem = Me.txtPageNumber
        Me.bindingNavigator.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.bindingNavigator.Size = New System.Drawing.Size(201, 25)
        Me.bindingNavigator.TabIndex = 334
        Me.bindingNavigator.Text = "PagerPanel"
        '
        'txtTotalPageNumber
        '
        Me.txtTotalPageNumber.Name = "txtTotalPageNumber"
        Me.txtTotalPageNumber.Size = New System.Drawing.Size(21, 22)
        Me.txtTotalPageNumber.Text = "of "
        Me.txtTotalPageNumber.ToolTipText = "Total number of items"
        '
        'BindingNavigatorMoveFirstItem
        '
        Me.BindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveFirstItem.Image = CType(resources.GetObject("BindingNavigatorMoveFirstItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveFirstItem.Name = "BindingNavigatorMoveFirstItem"
        Me.BindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveFirstItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveFirstItem.Text = "Move first"
        '
        'BindingNavigatorMovePreviousItem
        '
        Me.BindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMovePreviousItem.Image = CType(resources.GetObject("BindingNavigatorMovePreviousItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMovePreviousItem.Name = "BindingNavigatorMovePreviousItem"
        Me.BindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMovePreviousItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMovePreviousItem.Text = "Move previous"
        '
        'BindingNavigatorSeparator
        '
        Me.BindingNavigatorSeparator.Name = "BindingNavigatorSeparator"
        Me.BindingNavigatorSeparator.Size = New System.Drawing.Size(6, 25)
        '
        'txtPageNumber
        '
        Me.txtPageNumber.AccessibleName = "Position"
        Me.txtPageNumber.AutoSize = False
        Me.txtPageNumber.Name = "txtPageNumber"
        Me.txtPageNumber.Size = New System.Drawing.Size(30, 23)
        Me.txtPageNumber.Text = "0"
        Me.txtPageNumber.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtPageNumber.ToolTipText = "Current page"
        '
        'BindingNavigatorSeparator1
        '
        Me.BindingNavigatorSeparator1.Name = "BindingNavigatorSeparator1"
        Me.BindingNavigatorSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'BindingNavigatorMoveNextItem
        '
        Me.BindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveNextItem.Image = CType(resources.GetObject("BindingNavigatorMoveNextItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveNextItem.Name = "BindingNavigatorMoveNextItem"
        Me.BindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveNextItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveNextItem.Text = "Move next"
        '
        'BindingNavigatorMoveLastItem
        '
        Me.BindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.BindingNavigatorMoveLastItem.Image = CType(resources.GetObject("BindingNavigatorMoveLastItem.Image"), System.Drawing.Image)
        Me.BindingNavigatorMoveLastItem.Name = "BindingNavigatorMoveLastItem"
        Me.BindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = True
        Me.BindingNavigatorMoveLastItem.Size = New System.Drawing.Size(23, 22)
        Me.BindingNavigatorMoveLastItem.Text = "Move last"
        '
        'ToolStripSeparator
        '
        Me.ToolStripSeparator.Name = "ToolStripSeparator"
        Me.ToolStripSeparator.Size = New System.Drawing.Size(6, 25)
        '
        'btnGo
        '
        Me.btnGo.AutoSize = False
        Me.btnGo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnGo.Image = CType(resources.GetObject("btnGo.Image"), System.Drawing.Image)
        Me.btnGo.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(35, 22)
        Me.btnGo.Text = "Go"
        Me.btnGo.ToolTipText = "Go to page number specified"
        '
        'btnSync
        '
        Me.btnSync.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSync.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnSync.DefaultScheme = False
        Me.btnSync.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnSync.Font = New System.Drawing.Font("Verdana", 10.5!)
        Me.btnSync.Hint = "Synchronize items from Jeonsoft"
        Me.btnSync.Image = Global.LeaveFilingSystem.My.Resources.Resources.Sync_16_x_16
        Me.btnSync.Location = New System.Drawing.Point(800, 518)
        Me.btnSync.Name = "btnSync"
        Me.btnSync.Scheme = PinkieControls.ButtonXP.Schemes.Blue
        Me.btnSync.Size = New System.Drawing.Size(100, 36)
        Me.btnSync.TabIndex = 538
        Me.btnSync.TabStop = False
        Me.btnSync.Text = " Sync"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnSave.DefaultScheme = False
        Me.btnSave.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnSave.Font = New System.Drawing.Font("Verdana", 10.5!)
        Me.btnSave.Hint = "Save changes"
        Me.btnSave.Image = Global.LeaveFilingSystem.My.Resources.Resources.Save_16_x_16
        Me.btnSave.Location = New System.Drawing.Point(1008, 518)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Scheme = PinkieControls.ButtonXP.Schemes.Blue
        Me.btnSave.Size = New System.Drawing.Size(100, 36)
        Me.btnSave.TabIndex = 537
        Me.btnSave.TabStop = False
        Me.btnSave.Text = " Save"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnClose.DefaultScheme = False
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Verdana", 10.5!)
        Me.btnClose.Hint = "Close"
        Me.btnClose.Location = New System.Drawing.Point(1320, 518)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Scheme = PinkieControls.ButtonXP.Schemes.Blue
        Me.btnClose.Size = New System.Drawing.Size(100, 36)
        Me.btnClose.TabIndex = 536
        Me.btnClose.TabStop = False
        Me.btnClose.Text = "Close"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnDelete.DefaultScheme = False
        Me.btnDelete.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnDelete.Enabled = False
        Me.btnDelete.Font = New System.Drawing.Font("Verdana", 10.5!)
        Me.btnDelete.Hint = "Delete item"
        Me.btnDelete.Image = Global.LeaveFilingSystem.My.Resources.Resources.Erase_16_x_16
        Me.btnDelete.Location = New System.Drawing.Point(1216, 518)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Scheme = PinkieControls.ButtonXP.Schemes.Blue
        Me.btnDelete.Size = New System.Drawing.Size(100, 36)
        Me.btnDelete.TabIndex = 535
        Me.btnDelete.TabStop = False
        Me.btnDelete.Text = "Delete"
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnCancel.DefaultScheme = False
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnCancel.Font = New System.Drawing.Font("Verdana", 10.5!)
        Me.btnCancel.Hint = "Cancel changes"
        Me.btnCancel.Location = New System.Drawing.Point(1112, 518)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Scheme = PinkieControls.ButtonXP.Schemes.Blue
        Me.btnCancel.Size = New System.Drawing.Size(100, 36)
        Me.btnCancel.TabIndex = 534
        Me.btnCancel.TabStop = False
        Me.btnCancel.Text = "Cancel"
        '
        'btnAdd
        '
        Me.btnAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAdd.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnAdd.DefaultScheme = False
        Me.btnAdd.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnAdd.Enabled = False
        Me.btnAdd.Font = New System.Drawing.Font("Verdana", 10.5!)
        Me.btnAdd.Hint = "Add new item"
        Me.btnAdd.Image = Global.LeaveFilingSystem.My.Resources.Resources.Create_16_x_16
        Me.btnAdd.Location = New System.Drawing.Point(904, 518)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Scheme = PinkieControls.ButtonXP.Schemes.Blue
        Me.btnAdd.Size = New System.Drawing.Size(100, 36)
        Me.btnAdd.TabIndex = 533
        Me.btnAdd.TabStop = False
        Me.btnAdd.Text = " Add"
        '
        'pnlTop
        '
        Me.pnlTop.Controls.Add(Me.pnlDate)
        Me.pnlTop.Controls.Add(Me.btnReset)
        Me.pnlTop.Controls.Add(Me.btnSearch)
        Me.pnlTop.Controls.Add(Me.lblSearchCriteria)
        Me.pnlTop.Controls.Add(Me.cmbSearchCriteria)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(1426, 37)
        Me.pnlTop.TabIndex = 539
        '
        'pnlDate
        '
        Me.pnlDate.BackColor = System.Drawing.Color.White
        Me.pnlDate.Controls.Add(Me.txtName)
        Me.pnlDate.Location = New System.Drawing.Point(257, 2)
        Me.pnlDate.Name = "pnlDate"
        Me.pnlDate.Size = New System.Drawing.Size(348, 32)
        Me.pnlDate.TabIndex = 534
        '
        'txtName
        '
        Me.txtName.Font = New System.Drawing.Font("Verdana", 9.5!)
        Me.txtName.Location = New System.Drawing.Point(8, 5)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(333, 23)
        Me.txtName.TabIndex = 541
        '
        'btnReset
        '
        Me.btnReset.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.btnReset.DefaultScheme = False
        Me.btnReset.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnReset.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.btnReset.Hint = "Remove search filter"
        Me.btnReset.Image = Global.LeaveFilingSystem.My.Resources.Resources.Undo_16_x_16
        Me.btnReset.Location = New System.Drawing.Point(693, 5)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Scheme = PinkieControls.ButtonXP.Schemes.Blue
        Me.btnReset.Size = New System.Drawing.Size(85, 27)
        Me.btnReset.TabIndex = 537
        Me.btnReset.TabStop = False
        Me.btnReset.Text = "Reset"
        '
        'btnSearch
        '
        Me.btnSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer), CType(CType(240, Byte), Integer))
        Me.btnSearch.DefaultScheme = False
        Me.btnSearch.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnSearch.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.btnSearch.Hint = "Search"
        Me.btnSearch.Image = Global.LeaveFilingSystem.My.Resources.Resources.Find_16_x_16
        Me.btnSearch.Location = New System.Drawing.Point(605, 5)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Scheme = PinkieControls.ButtonXP.Schemes.Blue
        Me.btnSearch.Size = New System.Drawing.Size(85, 27)
        Me.btnSearch.TabIndex = 536
        Me.btnSearch.TabStop = False
        Me.btnSearch.Text = "Search"
        '
        'lblSearchCriteria
        '
        Me.lblSearchCriteria.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblSearchCriteria.BackColor = System.Drawing.SystemColors.Control
        Me.lblSearchCriteria.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSearchCriteria.ForeColor = System.Drawing.Color.Black
        Me.lblSearchCriteria.Location = New System.Drawing.Point(6, 6)
        Me.lblSearchCriteria.Name = "lblSearchCriteria"
        Me.lblSearchCriteria.Size = New System.Drawing.Size(65, 24)
        Me.lblSearchCriteria.TabIndex = 531
        Me.lblSearchCriteria.Text = "Criteria"
        Me.lblSearchCriteria.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmbSearchCriteria
        '
        Me.cmbSearchCriteria.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbSearchCriteria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSearchCriteria.Font = New System.Drawing.Font("Verdana", 9.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSearchCriteria.FormattingEnabled = True
        Me.cmbSearchCriteria.Location = New System.Drawing.Point(70, 6)
        Me.cmbSearchCriteria.Name = "cmbSearchCriteria"
        Me.cmbSearchCriteria.Size = New System.Drawing.Size(185, 24)
        Me.cmbSearchCriteria.TabIndex = 532
        '
        'dgvList
        '
        Me.dgvList.AllowUserToAddRows = False
        Me.dgvList.AllowUserToDeleteRows = False
        Me.dgvList.AllowUserToResizeRows = False
        Me.dgvList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Verdana", 8.5!)
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        Me.dgvList.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvList.ColumnHeadersHeight = 26
        Me.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColEmployeeId, Me.ColEmployeeCode, Me.ColEmployeeName, Me.ColPassword, Me.ColNbcEmailAddress, Me.ColIsApprover, Me.ColIsHrRecords, Me.ColIsEmployee, Me.ColIsHoliday, Me.ColIsActive})
        Me.dgvList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter
        Me.dgvList.Location = New System.Drawing.Point(0, 37)
        Me.dgvList.MultiSelect = False
        Me.dgvList.Name = "dgvList"
        Me.dgvList.RowHeadersVisible = False
        Me.dgvList.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Verdana", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvList.Size = New System.Drawing.Size(1426, 475)
        Me.dgvList.TabIndex = 540
        '
        'ColEmployeeId
        '
        Me.ColEmployeeId.DataPropertyName = "EmployeeId"
        Me.ColEmployeeId.HeaderText = "EmployeeId"
        Me.ColEmployeeId.Name = "ColEmployeeId"
        Me.ColEmployeeId.Visible = False
        '
        'ColEmployeeCode
        '
        Me.ColEmployeeCode.DataPropertyName = "EmployeeCode"
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.LightGray
        Me.ColEmployeeCode.DefaultCellStyle = DataGridViewCellStyle2
        Me.ColEmployeeCode.HeaderText = "Employee Code"
        Me.ColEmployeeCode.Name = "ColEmployeeCode"
        Me.ColEmployeeCode.ReadOnly = True
        Me.ColEmployeeCode.Width = 130
        '
        'ColEmployeeName
        '
        Me.ColEmployeeName.DataPropertyName = "EmployeeName"
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.LightGray
        Me.ColEmployeeName.DefaultCellStyle = DataGridViewCellStyle3
        Me.ColEmployeeName.HeaderText = "Employee Name"
        Me.ColEmployeeName.Name = "ColEmployeeName"
        Me.ColEmployeeName.ReadOnly = True
        Me.ColEmployeeName.Width = 200
        '
        'ColPassword
        '
        Me.ColPassword.DataPropertyName = "Password"
        Me.ColPassword.HeaderText = "Password"
        Me.ColPassword.Name = "ColPassword"
        Me.ColPassword.Width = 120
        '
        'ColNbcEmailAddress
        '
        Me.ColNbcEmailAddress.DataPropertyName = "NbcEmailAddress"
        Me.ColNbcEmailAddress.HeaderText = "NBC Email Address"
        Me.ColNbcEmailAddress.Name = "ColNbcEmailAddress"
        Me.ColNbcEmailAddress.Width = 150
        '
        'ColIsApprover
        '
        Me.ColIsApprover.DataPropertyName = "IsApprover"
        Me.ColIsApprover.HeaderText = "Approver"
        Me.ColIsApprover.Name = "ColIsApprover"
        Me.ColIsApprover.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ColIsApprover.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'ColIsHrRecords
        '
        Me.ColIsHrRecords.DataPropertyName = "IsHrRecords"
        Me.ColIsHrRecords.HeaderText = "HR Records"
        Me.ColIsHrRecords.Name = "ColIsHrRecords"
        Me.ColIsHrRecords.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ColIsHrRecords.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'ColIsEmployee
        '
        Me.ColIsEmployee.DataPropertyName = "IsEmployee"
        Me.ColIsEmployee.HeaderText = "Employee"
        Me.ColIsEmployee.Name = "ColIsEmployee"
        Me.ColIsEmployee.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ColIsEmployee.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'ColIsHoliday
        '
        Me.ColIsHoliday.DataPropertyName = "IsHoliday"
        Me.ColIsHoliday.HeaderText = "Holiday"
        Me.ColIsHoliday.Name = "ColIsHoliday"
        Me.ColIsHoliday.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ColIsHoliday.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'ColIsActive
        '
        Me.ColIsActive.DataPropertyName = "IsActive"
        Me.ColIsActive.HeaderText = "Active"
        Me.ColIsActive.Name = "ColIsActive"
        Me.ColIsActive.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ColIsActive.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'frmEmployee
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(1426, 561)
        Me.Controls.Add(Me.dgvList)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.btnSync)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.bindingNavigator)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Verdana", 8.5!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmEmployee"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Employees"
        CType(Me.bindingNavigator, System.ComponentModel.ISupportInitialize).EndInit()
        Me.bindingNavigator.ResumeLayout(False)
        Me.bindingNavigator.PerformLayout()
        Me.pnlTop.ResumeLayout(False)
        Me.pnlDate.ResumeLayout(False)
        Me.pnlDate.PerformLayout()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents bindingNavigator As System.Windows.Forms.BindingNavigator
    Friend WithEvents txtTotalPageNumber As System.Windows.Forms.ToolStripLabel
    Friend WithEvents BindingNavigatorMoveFirstItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMovePreviousItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents txtPageNumber As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents BindingNavigatorSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorMoveNextItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMoveLastItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnGo As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSync As PinkieControls.ButtonXP
    Friend WithEvents btnSave As PinkieControls.ButtonXP
    Friend WithEvents btnClose As PinkieControls.ButtonXP
    Friend WithEvents btnDelete As PinkieControls.ButtonXP
    Friend WithEvents btnCancel As PinkieControls.ButtonXP
    Friend WithEvents btnAdd As PinkieControls.ButtonXP
    Friend WithEvents pnlTop As System.Windows.Forms.Panel
    Friend WithEvents pnlDate As System.Windows.Forms.Panel
    Friend WithEvents btnReset As PinkieControls.ButtonXP
    Friend WithEvents btnSearch As PinkieControls.ButtonXP
    Friend WithEvents lblSearchCriteria As System.Windows.Forms.Label
    Friend WithEvents cmbSearchCriteria As System.Windows.Forms.ComboBox
    Friend WithEvents dgvList As System.Windows.Forms.DataGridView
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents ColEmployeeId As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColEmployeeCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColEmployeeName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColPassword As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColNbcEmailAddress As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColIsApprover As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents ColIsHrRecords As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents ColIsEmployee As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents ColIsHoliday As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents ColIsActive As System.Windows.Forms.DataGridViewCheckBoxColumn
End Class
