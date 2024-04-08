using LSSERVICEPROVIDERLib;
using Patholab_DAL_V1;
using Patholab_DAL_V1.Enums;
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls;
using System.IO;

namespace ExtraMaterialControl
{
    public partial class ExtraMaterialForm : Telerik.WinControls.UI.RadForm
    {
        internal INautilusServiceProvider _sp;
        internal INautilusUser _ntlsUser;
        internal DataLayer _dal;
        internal SDG _sdg;
        internal long sid;
        private const int TreeItemHeight = 19;
        //private Dictionary<string, bool> canRescan;

        public ExtraMaterialForm(DataLayer dal, SDG sdg, INautilusServiceProvider sp, INautilusUser ntlsUser, long sid)
        {
            InitializeComponent();

            this._dal = dal;
            this._sdg = sdg;
            this._sp = sp;
            this.sid = sid;
            this._ntlsUser = Patholab_Common.Utils.GetNautilusUser(_sp);
            this.Text = sdg.NAME;
            sdgTree.ItemHeight = TreeItemHeight;
            //canRescan = new Dictionary<string, bool>();

            LoadTree();
        }

        //Load sdg
        public void LoadTree()
        {
            //Clear
            sdgTree.Nodes.Clear();
            //canRescan.Clear();

            string path = Patholab_Common.Utils.GetResourcePath();
            var samples = _dal.FindBy<SAMPLE>(sm => sm.SDG_ID == _sdg.SDG_ID);

            foreach (var sample in samples)
            {

                sdgTree.Nodes.Add(sample.NAME);

                var sampleNode = sdgTree.FindNodes(sample.NAME).First();
                sampleNode.Image = new Bitmap(Path.Combine(path + "sample" + sample.STATUS + ".ico"));

                //Get blocks
                //var blocks = sample.ALIQUOTs.Where(x => x.ALIQ_FORMULATION_PARENT.Count < 1).OrderBy(al => al.ALIQUOT_ID);

                //foreach (var aliquot in blocks)
                //{

                //    sampleNode.Nodes.Add(aliquot.NAME);

                //    //if(!canRescan.ContainsKey(aliquot.NAME))
                //        //canRescan.Add(aliquot.NAME, aliquot.STATUS != "X");

                //    var nd = sdgTree.FindNodes(aliquot.NAME).First();

                //    if (nd != null)
                //        nd.Image = new Bitmap(Path.Combine(path + "aliquot" + aliquot.STATUS + ".ico"));

                //    //Get Slides
                //    var children = aliquot.ALIQ_FORMULATION_CHILD.OrderBy(a => a.CHILD_ALIQUOT_ID);

                //    foreach (var child in children)
                //    {
                //        string nodeName = string.Format("{0} ({1})", child.PARENT.NAME, child.PARENT.ALIQUOT_USER.U_COLOR_TYPE);

                //        nd.Nodes.Add(nodeName);

                //        //if (!canRescan.ContainsKey(child.PARENT.NAME))
                //            //canRescan.Add(child.PARENT.NAME, child.PARENT.STATUS != "X");

                //        sdgTree.FindNodes(nodeName).First().Image = new Bitmap(Path.Combine(path + "aliquot" + child.PARENT.STATUS + ".ico"));
                //    }
                //}
            }

            sdgTree.ExpandAll();
        }



        private void btnAddSynamic_Click(object sender, EventArgs e)
        {
            if (sdgTree.SelectedNode != null)
            {
                string nameOnly = sdgTree.SelectedNode.Text.Split('(')[0].Trim();

                if (!lbox_entities.Items.Contains(nameOnly))
                    lbox_entities.Items.Add(nameOnly);
            }

            lblCont4adv.Text = lbox_entities.Items.Count.ToString();
        }

        private void btnRemoveDynamic_Click(object sender, EventArgs e)
        {
            if (lbox_entities.SelectedItem != null)
            {
                lbox_entities.Items.Remove(lbox_entities.SelectedItem);
            }

            lblCont4adv.Text = lbox_entities.Items.Count.ToString();
        }
        Boolean flag_if_request_was_sent = false;
        

        private void btnSendExtraMaterial_Click(object sender, EventArgs e)
        {
            HideLabels();

            if (lbox_entities.Items.Count < 1)
            {
                MessageBox.Show(" אנא בחר ישות");
                return;
            }

            const string successMsg = "The request was saved successfully.";
            int countAddedRequest = 0;
            lblMsgAdv.Text = string.Empty;

            foreach (var item in lbox_entities.Items)
            {
                var entityName = item.ToString();



                long currentOperatorId = Convert.ToInt64(_ntlsUser.GetOperatorId());
                var details = textBoxDetails.Text;


                _dal.Ex_Req_Logic(_sdg.SDG_ID, entityName, ExtraRequestType.M, currentOperatorId, textBoxDetails.Text, textBoxDetails.Text);

                //setAliquotStationField(entityName);

                lblMsgAdv.Text += Environment.NewLine + entityName + " added successfully.";
                countAddedRequest++;


            }

            LoadTree();
            //כאן הוא בעצם בודק אם נוספה בקשה?
            lblMsgAdv.Text = countAddedRequest == lbox_entities.Items.Count ? successMsg : lblMsgAdv.Text;

           

            lblMsgAdv.Visible = true;
        }

        private void setAliquotStationField(string aliquotName)
        {
            //try
            //{
            //    var aliquotUser = _dal.FindBy<ALIQUOT_USER>(a => a.ALIQUOT.NAME.Equals(aliquotName)).FirstOrDefault();

            //    if (aliquotUser != null)
            //    {
            //        var currentStation = aliquotUser.U_ALIQUOT_STATION;
            //        aliquotUser.U_ALIQUOT_STATION = "131";

            //        if (currentStation != null)
            //        {
            //            aliquotUser.U_OLD_ALIQUOT_STATION += currentStation;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    return;
            //}

        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        void HideLabels()
        {
            lblMsgAdv.Visible = false;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblMsgAdv_Click(object sender, EventArgs e)
        {

        }
    }
}
