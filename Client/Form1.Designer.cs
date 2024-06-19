namespace Client
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtReceiver = new System.Windows.Forms.TextBox();
            this.btnJoin = new System.Windows.Forms.Button();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.lbReceiver = new System.Windows.Forms.Label();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.lstMessages = new System.Windows.Forms.ListBox();
            this.txtPublickey_alpha = new System.Windows.Forms.TextBox();
            this.txtPublickey_beta = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPrivatekey_a = new System.Windows.Forms.TextBox();
            this.txtPublickey_p = new System.Windows.Forms.TextBox();
            this.lbHello = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtReceiver
            // 
            this.txtReceiver.Location = new System.Drawing.Point(554, 318);
            this.txtReceiver.Name = "txtReceiver";
            this.txtReceiver.Size = new System.Drawing.Size(215, 22);
            this.txtReceiver.TabIndex = 30;
            // 
            // btnJoin
            // 
            this.btnJoin.AutoSize = true;
            this.btnJoin.Location = new System.Drawing.Point(226, 37);
            this.btnJoin.Name = "btnJoin";
            this.btnJoin.Size = new System.Drawing.Size(75, 26);
            this.btnJoin.TabIndex = 29;
            this.btnJoin.Text = "Login";
            this.btnJoin.UseVisualStyleBackColor = true;
            this.btnJoin.Click += new System.EventHandler(this.btnJoin_Click);
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(85, 41);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(135, 22);
            this.txtUsername.TabIndex = 28;
            // 
            // btnSend
            // 
            this.btnSend.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSend.Location = new System.Drawing.Point(621, 382);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 27;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // lbReceiver
            // 
            this.lbReceiver.AutoSize = true;
            this.lbReceiver.Location = new System.Drawing.Point(520, 322);
            this.lbReceiver.Name = "lbReceiver";
            this.lbReceiver.Size = new System.Drawing.Size(27, 16);
            this.lbReceiver.TabIndex = 26;
            this.lbReceiver.Text = "To:";
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(554, 354);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(215, 22);
            this.txtMessage.TabIndex = 25;
            // 
            // lstMessages
            // 
            this.lstMessages.FormattingEnabled = true;
            this.lstMessages.ItemHeight = 16;
            this.lstMessages.Location = new System.Drawing.Point(554, 44);
            this.lstMessages.Name = "lstMessages";
            this.lstMessages.Size = new System.Drawing.Size(215, 260);
            this.lstMessages.TabIndex = 24;
            // 
            // txtPublickey_alpha
            // 
            this.txtPublickey_alpha.Location = new System.Drawing.Point(78, 270);
            this.txtPublickey_alpha.Name = "txtPublickey_alpha";
            this.txtPublickey_alpha.ReadOnly = true;
            this.txtPublickey_alpha.Size = new System.Drawing.Size(142, 22);
            this.txtPublickey_alpha.TabIndex = 23;
            // 
            // txtPublickey_beta
            // 
            this.txtPublickey_beta.Location = new System.Drawing.Point(78, 298);
            this.txtPublickey_beta.Name = "txtPublickey_beta";
            this.txtPublickey_beta.ReadOnly = true;
            this.txtPublickey_beta.Size = new System.Drawing.Size(142, 22);
            this.txtPublickey_beta.TabIndex = 22;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(75, 366);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 16);
            this.label2.TabIndex = 20;
            this.label2.Text = "Private key:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(78, 220);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 19;
            this.label1.Text = "Public key:";
            // 
            // txtPrivatekey_a
            // 
            this.txtPrivatekey_a.Location = new System.Drawing.Point(78, 391);
            this.txtPrivatekey_a.Name = "txtPrivatekey_a";
            this.txtPrivatekey_a.ReadOnly = true;
            this.txtPrivatekey_a.Size = new System.Drawing.Size(142, 22);
            this.txtPrivatekey_a.TabIndex = 18;
            // 
            // txtPublickey_p
            // 
            this.txtPublickey_p.Location = new System.Drawing.Point(78, 242);
            this.txtPublickey_p.Name = "txtPublickey_p";
            this.txtPublickey_p.ReadOnly = true;
            this.txtPublickey_p.Size = new System.Drawing.Size(142, 22);
            this.txtPublickey_p.TabIndex = 17;
            // 
            // lbHello
            // 
            this.lbHello.AutoSize = true;
            this.lbHello.Location = new System.Drawing.Point(32, 44);
            this.lbHello.Name = "lbHello";
            this.lbHello.Size = new System.Drawing.Size(47, 16);
            this.lbHello.TabIndex = 16;
            this.lbHello.Text = "Name:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtReceiver);
            this.Controls.Add(this.btnJoin);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.lbReceiver);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.lstMessages);
            this.Controls.Add(this.txtPublickey_alpha);
            this.Controls.Add(this.txtPublickey_beta);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPrivatekey_a);
            this.Controls.Add(this.txtPublickey_p);
            this.Controls.Add(this.lbHello);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtReceiver;
        private System.Windows.Forms.Button btnJoin;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label lbReceiver;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.ListBox lstMessages;
        private System.Windows.Forms.TextBox txtPublickey_alpha;
        private System.Windows.Forms.TextBox txtPublickey_beta;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPrivatekey_a;
        private System.Windows.Forms.TextBox txtPublickey_p;
        private System.Windows.Forms.Label lbHello;
    }
}

