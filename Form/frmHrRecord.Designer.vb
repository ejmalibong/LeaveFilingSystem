<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHrRecord
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmHrRecord))
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle12 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle13 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle14 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.btnClose = New PinkieControls.ButtonXP()
        Me.btnView = New PinkieControls.ButtonXP()
        Me.grpCriteria = New System.Windows.Forms.GroupBox()
        Me.bindingNavigator = New System.Windows.Forms.BindingNavigator(Me.components)
        Me.txtTotalPageNumber = New System.Windows.Forms.ToolStripLabel()
        Me.BindingNavigatorMoveFirstItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMovePreviousItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.txtPageNumber = New System.Windows.Forms.ToolStripTextBox()
        Me.BindingNavigatorSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.BindingNavigatorMoveNextItem = New System.Windows.Forms.ToolStripButton()
        Me.BindingNavigatorMoveLastItem = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnGo = New System.Windows.Forms.ToolStripButton()
        Me.dgvList = New System.Windows.Forms.DataGridView()
        Me.btnRefresh = New PinkieControls.ButtonXP()
        Me.btnDisapprove = New PinkieControls.ButtonXP()
        Me.btnApprove = New PinkieControls.ButtonXP()
        Me.grpStatus = New System.Windows.Forms.GroupBox()
        Me.rdDisapproved = New System.Windows.Forms.RadioButton()
        Me.rdPending = New System.Windows.Forms.RadioButton()
        Me.rdApproved = New System.Windows.Forms.RadioButton()
        Me.ColLeaveFileId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColDateCreated = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColLeaveTypeId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColLeaveTypeName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColStartDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColEndDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColQuantity = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColReason = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RoutingStatusId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ColRoutingStatusName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.grpCriteria.SuspendLayout()
        CType(Me.bindingNavigator, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.bindingNavigator.SuspendLayout()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpStatus.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnClose.DefaultScheme = False
        Me.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnClose.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.btnClose.Hint = "Close Leave Records"
        Me.btnClose.Location = New System.Drawing.Point(1166, 519)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Scheme = PinkieControls.ButtonXP.Schemes.Blue
        Me.btnClose.Size = New System.Drawing.Size(110, 36)
        Me.btnClose.TabIndex = 155
        Me.btnClose.Text = "Close"
        '
        'btnView
        '
        Me.btnView.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnView.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnView.DefaultScheme = False
        Me.btnView.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnView.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.btnView.Hint = "View / Edit Record"
        Me.btnView.Location = New System.Drawing.Point(1000, 519)
        Me.btnView.Name = "btnView"
        Me.btnView.Scheme = PinkieControls.ButtonXP.Schemes.Blue
        Me.btnView.Size = New System.Drawing.Size(160, 36)
        Me.btnView.TabIndex = 153
        Me.btnView.Text = "    View Details"
        '
        'grpCriteria
        '
        Me.grpCriteria.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpCriteria.Controls.Add(Me.bindingNavigator)
        Me.grpCriteria.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.grpCriteria.Location = New System.Drawing.Point(1, -6)
        Me.grpCriteria.Margin = New System.Windows.Forms.Padding(0)
        Me.grpCriteria.Name = "grpCriteria"
        Me.grpCriteria.Size = New System.Drawing.Size(1282, 44)
        Me.grpCriteria.TabIndex = 157
        Me.grpCriteria.TabStop = False
        '
        'bindingNavigator
        '
        Me.bindingNavigator.AddNewItem = Nothing
        Me.bindingNavigator.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.bindingNavigator.BackColor = System.Drawing.Color.White
        Me.bindingNavigator.CountItem = Me.txtTotalPageNumber
        Me.bindingNavigator.CountItemFormat = "of "
        Me.bindingNavigator.DeleteItem = Nothing
        Me.bindingNavigator.Dock = System.Windows.Forms.DockStyle.None
        Me.bindingNavigator.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.bindingNavigator.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.bindingNavigator.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BindingNavigatorMoveFirstItem, Me.BindingNavigatorMovePreviousItem, Me.BindingNavigatorSeparator, Me.txtPageNumber, Me.BindingNavigatorSeparator1, Me.txtTotalPageNumber, Me.BindingNavigatorMoveNextItem, Me.BindingNavigatorMoveLastItem, Me.ToolStripSeparator1, Me.btnGo})
        Me.bindingNavigator.Location = New System.Drawing.Point(1078, 12)
        Me.bindingNavigator.MoveFirstItem = Me.BindingNavigatorMoveFirstItem
        Me.bindingNavigator.MoveLastItem = Me.BindingNavigatorMoveLastItem
        Me.bindingNavigator.MoveNextItem = Me.BindingNavigatorMoveNextItem
        Me.bindingNavigator.MovePreviousItem = Me.BindingNavigatorMovePreviousItem
        Me.bindingNavigator.Name = "bindingNavigator"
        Me.bindingNavigator.PositionItem = Me.txtPageNumber
        Me.bindingNavigator.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.bindingNavigator.Size = New System.Drawing.Size(201, 25)
        Me.bindingNavigator.TabIndex = 11
        Me.bindingNavigator.Text = "PagerPanel"
        '
        'txtTotalPageNumber
        '
        Me.txtTotalPageNumber.Name = "txtTotalPageNumber"
        Me.txtTotalPageNumber.Size = New System.Drawing.Size(21, 22)
        Me.txtTotalPageNumber.Text = "of "
        Me.txtTotalPageNumber.ToolTipText = "Total number of pages"
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
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
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
        'dgvList
        '
        Me.dgvList.AllowUserToAddRows = False
        Me.dgvList.AllowUserToDeleteRows = False
        Me.dgvList.AllowUserToResizeRows = False
        Me.dgvList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Verdana", 8.5!)
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvList.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle8
        Me.dgvList.ColumnHeadersHeight = 25
        Me.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.dgvList.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ColLeaveFileId, Me.ColDateCreated, Me.ColLeaveTypeId, Me.ColLeaveTypeName, Me.ColName, Me.ColStartDate, Me.ColEndDate, Me.ColQuantity, Me.ColReason, Me.RoutingStatusId, Me.ColRoutingStatusName})
        Me.dgvList.Location = New System.Drawing.Point(0, 38)
        Me.dgvList.MultiSelect = False
        Me.dgvList.Name = "dgvList"
        Me.dgvList.ReadOnly = True
        Me.dgvList.RowHeadersVisible = False
        Me.dgvList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.dgvList.RowTemplate.DefaultCellStyle.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgvList.Size = New System.Drawing.Size(1284, 475)
        Me.dgvList.TabIndex = 158
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnRefresh.DefaultScheme = False
        Me.btnRefresh.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnRefresh.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.btnRefresh.Hint = "Refresh"
        Me.btnRefresh.Image = Global.LeaveFilingSystem.My.Resources.Resources.Refresh_16_x_16
        Me.btnRefresh.Location = New System.Drawing.Point(318, 519)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Scheme = PinkieControls.ButtonXP.Schemes.Blue
        Me.btnRefresh.Size = New System.Drawing.Size(110, 36)
        Me.btnRefresh.TabIndex = 160
        Me.btnRefresh.Text = " Refresh"
        '
        'btnDisapprove
        '
        Me.btnDisapprove.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDisapprove.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnDisapprove.DefaultScheme = False
        Me.btnDisapprove.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnDisapprove.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.btnDisapprove.Hint = "Disapprove Leave"
        Me.btnDisapprove.Image = Global.LeaveFilingSystem.My.Resources.Resources.Delete_16_x_16
        Me.btnDisapprove.Location = New System.Drawing.Point(550, 519)
        Me.btnDisapprove.Name = "btnDisapprove"
        Me.btnDisapprove.Scheme = PinkieControls.ButtonXP.Schemes.Blue
        Me.btnDisapprove.Size = New System.Drawing.Size(110, 36)
        Me.btnDisapprove.TabIndex = 162
        Me.btnDisapprove.Text = "Disapprove"
        '
        'btnApprove
        '
        Me.btnApprove.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnApprove.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.btnApprove.DefaultScheme = False
        Me.btnApprove.DialogResult = System.Windows.Forms.DialogResult.None
        Me.btnApprove.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.btnApprove.Hint = "Approve Leave"
        Me.btnApprove.Image = Global.LeaveFilingSystem.My.Resources.Resources.Apply_16_x_16
        Me.btnApprove.Location = New System.Drawing.Point(434, 519)
        Me.btnApprove.Name = "btnApprove"
        Me.btnApprove.Scheme = PinkieControls.ButtonXP.Schemes.Blue
        Me.btnApprove.Size = New System.Drawing.Size(110, 36)
        Me.btnApprove.TabIndex = 161
        Me.btnApprove.Text = " Approve"
        '
        'grpStatus
        '
        Me.grpStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grpStatus.BackColor = System.Drawing.Color.White
        Me.grpStatus.Controls.Add(Me.rdDisapproved)
        Me.grpStatus.Controls.Add(Me.rdPending)
        Me.grpStatus.Controls.Add(Me.rdApproved)
        Me.grpStatus.Location = New System.Drawing.Point(7, 513)
        Me.grpStatus.Margin = New System.Windows.Forms.Padding(0)
        Me.grpStatus.Name = "grpStatus"
        Me.grpStatus.Padding = New System.Windows.Forms.Padding(0)
        Me.grpStatus.Size = New System.Drawing.Size(306, 42)
        Me.grpStatus.TabIndex = 163
        Me.grpStatus.TabStop = False
        '
        'rdDisapproved
        '
        Me.rdDisapproved.AutoSize = True
        Me.rdDisapproved.Location = New System.Drawing.Point(9, 15)
        Me.rdDisapproved.Name = "rdDisapproved"
        Me.rdDisapproved.Size = New System.Drawing.Size(104, 18)
        Me.rdDisapproved.TabIndex = 4
        Me.rdDisapproved.TabStop = True
        Me.rdDisapproved.Text = "Disapproved"
        Me.rdDisapproved.UseVisualStyleBackColor = True
        '
        'rdPending
        '
        Me.rdPending.AutoSize = True
        Me.rdPending.Location = New System.Drawing.Point(222, 15)
        Me.rdPending.Name = "rdPending"
        Me.rdPending.Size = New System.Drawing.Size(76, 18)
        Me.rdPending.TabIndex = 2
        Me.rdPending.TabStop = True
        Me.rdPending.Text = "Pending"
        Me.rdPending.UseVisualStyleBackColor = True
        '
        'rdApproved
        '
        Me.rdApproved.AutoSize = True
        Me.rdApproved.Location = New System.Drawing.Point(124, 15)
        Me.rdApproved.Name = "rdApproved"
        Me.rdApproved.Size = New System.Drawing.Size(85, 18)
        Me.rdApproved.TabIndex = 1
        Me.rdApproved.TabStop = True
        Me.rdApproved.Text = "Approved"
        Me.rdApproved.UseVisualStyleBackColor = True
        '
        'ColLeaveFileId
        '
        Me.ColLeaveFileId.DataPropertyName = "LeaveFileId"
        Me.ColLeaveFileId.HeaderText = "LeaveFilingId"
        Me.ColLeaveFileId.Name = "ColLeaveFileId"
        Me.ColLeaveFileId.ReadOnly = True
        Me.ColLeaveFileId.Visible = False
        '
        'ColDateCreated
        '
        Me.ColDateCreated.DataPropertyName = "DateCreated"
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.ColDateCreated.DefaultCellStyle = DataGridViewCellStyle9
        Me.ColDateCreated.HeaderText = "Date Created"
        Me.ColDateCreated.Name = "ColDateCreated"
        Me.ColDateCreated.ReadOnly = True
        Me.ColDateCreated.Width = 120
        '
        'ColLeaveTypeId
        '
        Me.ColLeaveTypeId.DataPropertyName = "LeaveTypeId"
        Me.ColLeaveTypeId.HeaderText = "LeaveTypeId"
        Me.ColLeaveTypeId.Name = "ColLeaveTypeId"
        Me.ColLeaveTypeId.ReadOnly = True
        Me.ColLeaveTypeId.Visible = False
        '
        'ColLeaveTypeName
        '
        Me.ColLeaveTypeName.DataPropertyName = "LeaveTypeName"
        Me.ColLeaveTypeName.HeaderText = "Leave Type"
        Me.ColLeaveTypeName.Name = "ColLeaveTypeName"
        Me.ColLeaveTypeName.ReadOnly = True
        Me.ColLeaveTypeName.Width = 120
        '
        'ColName
        '
        Me.ColName.DataPropertyName = "EmployeeName"
        Me.ColName.HeaderText = "Name"
        Me.ColName.Name = "ColName"
        Me.ColName.ReadOnly = True
        Me.ColName.Width = 240
        '
        'ColStartDate
        '
        Me.ColStartDate.DataPropertyName = "StartDate"
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.ColStartDate.DefaultCellStyle = DataGridViewCellStyle10
        Me.ColStartDate.HeaderText = "From"
        Me.ColStartDate.Name = "ColStartDate"
        Me.ColStartDate.ReadOnly = True
        Me.ColStartDate.Width = 90
        '
        'ColEndDate
        '
        Me.ColEndDate.DataPropertyName = "EndDate"
        DataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.ColEndDate.DefaultCellStyle = DataGridViewCellStyle11
        Me.ColEndDate.HeaderText = "To"
        Me.ColEndDate.Name = "ColEndDate"
        Me.ColEndDate.ReadOnly = True
        Me.ColEndDate.Width = 90
        '
        'ColQuantity
        '
        Me.ColQuantity.DataPropertyName = "Quantity"
        DataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.ColQuantity.DefaultCellStyle = DataGridViewCellStyle12
        Me.ColQuantity.HeaderText = "Qty"
        Me.ColQuantity.Name = "ColQuantity"
        Me.ColQuantity.ReadOnly = True
        Me.ColQuantity.Width = 50
        '
        'ColReason
        '
        Me.ColReason.DataPropertyName = "Reason"
        DataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.ColReason.DefaultCellStyle = DataGridViewCellStyle13
        Me.ColReason.HeaderText = "Reason"
        Me.ColReason.Name = "ColReason"
        Me.ColReason.ReadOnly = True
        Me.ColReason.Width = 265
        '
        'RoutingStatusId
        '
        Me.RoutingStatusId.DataPropertyName = "RoutingStatusId"
        Me.RoutingStatusId.HeaderText = "RoutingStatusId"
        Me.RoutingStatusId.Name = "RoutingStatusId"
        Me.RoutingStatusId.ReadOnly = True
        Me.RoutingStatusId.Visible = False
        '
        'ColRoutingStatusName
        '
        Me.ColRoutingStatusName.DataPropertyName = "RoutingStatusName"
        DataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        Me.ColRoutingStatusName.DefaultCellStyle = DataGridViewCellStyle14
        Me.ColRoutingStatusName.HeaderText = "Status"
        Me.ColRoutingStatusName.Name = "ColRoutingStatusName"
        Me.ColRoutingStatusName.ReadOnly = True
        Me.ColRoutingStatusName.Width = 280
        '
        'frmHrRecord
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.White
        Me.CancelButton = Me.btnClose
        Me.ClientSize = New System.Drawing.Size(1284, 561)
        Me.Controls.Add(Me.grpStatus)
        Me.Controls.Add(Me.btnDisapprove)
        Me.Controls.Add(Me.btnApprove)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.dgvList)
        Me.Controls.Add(Me.grpCriteria)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnView)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Verdana", 8.5!)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(1300, 600)
        Me.Name = "frmHrRecord"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "HR Records"
        Me.grpCriteria.ResumeLayout(False)
        Me.grpCriteria.PerformLayout()
        CType(Me.bindingNavigator, System.ComponentModel.ISupportInitialize).EndInit()
        Me.bindingNavigator.ResumeLayout(False)
        Me.bindingNavigator.PerformLayout()
        CType(Me.dgvList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpStatus.ResumeLayout(False)
        Me.grpStatus.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnClose As PinkieControls.ButtonXP
    Friend WithEvents btnView As PinkieControls.ButtonXP
    Friend WithEvents grpCriteria As System.Windows.Forms.GroupBox
    Friend WithEvents dgvList As System.Windows.Forms.DataGridView
    Friend WithEvents btnRefresh As PinkieControls.ButtonXP
    Friend WithEvents btnDisapprove As PinkieControls.ButtonXP
    Friend WithEvents btnApprove As PinkieControls.ButtonXP
    Friend WithEvents bindingNavigator As System.Windows.Forms.BindingNavigator
    Friend WithEvents txtTotalPageNumber As System.Windows.Forms.ToolStripLabel
    Friend WithEvents BindingNavigatorMoveFirstItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMovePreviousItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents txtPageNumber As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents BindingNavigatorSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BindingNavigatorMoveNextItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents BindingNavigatorMoveLastItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnGo As System.Windows.Forms.ToolStripButton
    Friend WithEvents grpStatus As System.Windows.Forms.GroupBox
    Friend WithEvents rdDisapproved As System.Windows.Forms.RadioButton
    Friend WithEvents rdPending As System.Windows.Forms.RadioButton
    Friend WithEvents rdApproved As System.Windows.Forms.RadioButton
    Friend WithEvents ColLeaveFileId As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColDateCreated As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColLeaveTypeId As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColLeaveTypeName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColStartDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColEndDate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColQuantity As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColReason As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RoutingStatusId As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColRoutingStatusName As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
