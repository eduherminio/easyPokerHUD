using System.Windows.Forms;

namespace easyPokerHUD
{
    partial class StatsWindow
    {
        public System.Windows.Forms.Label VPIP { get; private set; }

        public System.Windows.Forms.Label PFR { get; private set; }

        public System.Windows.Forms.Label AFq { get; private set; }

        public System.Windows.Forms.TableLayoutPanel TableLayoutPanel { get; private set; }

        public System.Windows.Forms.Label Username { get; private set; }

        public System.Windows.Forms.Label HandsPlayed { get; private set; }


        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TableLayoutPanel.SuspendLayout();
            this.SuspendLayout();

            this.VPIP = new System.Windows.Forms.Label()
            {
                Anchor = System.Windows.Forms.AnchorStyles.None,
                AutoSize = true,
                BackColor = System.Drawing.Color.Transparent,
                Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                ForeColor = System.Drawing.Color.White,
                Location = new System.Drawing.Point(3, 14),
                Name = "VPIP",
                Size = new System.Drawing.Size(25, 14),
                TabIndex = 0,
                Text = "100",
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            };

            this.PFR = new System.Windows.Forms.Label()
            {
                Anchor = System.Windows.Forms.AnchorStyles.None,
                AutoSize = true,
                BackColor = System.Drawing.Color.Transparent,
                Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                ForeColor = System.Drawing.Color.White,
                Location = new System.Drawing.Point(34, 14),
                Name = "PFR",
                Size = new System.Drawing.Size(25, 14),
                TabIndex = 1,
                Text = "100",
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            };

            this.AFq = new System.Windows.Forms.Label()
            {
                Anchor = System.Windows.Forms.AnchorStyles.None,
                AutoSize = true,
                BackColor = System.Drawing.Color.Transparent,
                Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                ForeColor = System.Drawing.Color.White,
                Location = new System.Drawing.Point(65, 14),
                Name = "AFq",
                Size = new System.Drawing.Size(25, 14),
                TabIndex = 2,
                Text = "100",
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
            };

            this.HandsPlayed = new System.Windows.Forms.Label()
            {
                AutoSize = true,
                BackColor = System.Drawing.Color.Transparent,
                Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                ForeColor = System.Drawing.Color.Gray,
                Location = new System.Drawing.Point(139, 1),
                Name = "handsplayed",
                Size = new System.Drawing.Size(25, 14),
                TabIndex = 3,
                Text = "100",
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            };

            this.Username = new System.Windows.Forms.Label()
            {
                AutoSize = true,
                BackColor = System.Drawing.Color.Transparent,
                Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                ForeColor = System.Drawing.Color.White,
                Location = new System.Drawing.Point(3, 0),
                Name = "Username",
                Size = new System.Drawing.Size(56, 14),
                TabIndex = 3,
                Text = "Username",
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter
            };

            this.TableLayoutPanel = new System.Windows.Forms.TableLayoutPanel()
            {
                AutoSize = true,
                AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink,
                BackColor = System.Drawing.Color.Transparent,
                ColumnCount = 4,
                Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                Location = new System.Drawing.Point(1, 1),
                Margin = new System.Windows.Forms.Padding(0),
                Name = "tableLayoutPanel1",
                RowCount = 2,
                Size = new System.Drawing.Size(93, 28),
                TabIndex = 4
            };

            TableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            TableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            TableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            TableLayoutPanel.ColumnStyles.Add(new ColumnStyle());
            TableLayoutPanel.RowStyles.Add(new RowStyle());
            TableLayoutPanel.RowStyles.Add(new RowStyle());
            TableLayoutPanel.Controls.Add(this.VPIP, 0, 0);
            TableLayoutPanel.Controls.Add(this.PFR, 1, 0);
            TableLayoutPanel.Controls.Add(this.AFq, 2, 0);
            TableLayoutPanel.Controls.Add(this.HandsPlayed, 3, 0);

            this.TableLayoutPanel.SetColumnSpan(this.Username, 2);

            //
            // StatsWindow
            //
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.TableLayoutPanel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "StatsWindow";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Size = new System.Drawing.Size(95, 30);
            this.SizeChanged += new System.EventHandler(this.StatsWindow_SizeChanged);
            this.TableLayoutPanel.ResumeLayout(false);
            this.TableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

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
    }
}
