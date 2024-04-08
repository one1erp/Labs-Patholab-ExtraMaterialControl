using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Patholab_DAL_V1;
using LSSERVICEPROVIDERLib;
using Telerik.WinControls.UI;
using System.Diagnostics;
using Patholab_Common;
using Telerik.WinControls.Primitives;

namespace ExtraMaterialControl
{
    public partial class ExtraMaterialCtrl : UserControl
    {

        internal INautilusServiceProvider _sp;
        internal INautilusUser _ntlsUser;
        private INautilusDBConnection _ntlsCon;
        internal DataLayer _dal;
        internal SDG _sdg = null;
        internal long sid;
        private SDG_DETAILS _sdgDtls, oldSDGdtls;
        public bool DEBUG;
        private Timer _bckgrndSaver;
        private int CUR_SDG_ID;
        
        
        public RadButton ExMaterialButton { get; set; }
     
        public ExtraMaterialCtrl()
        {
            InitializeComponent();
        

        }
        //my code (AE)
        public Boolean HasMaterialAddingReq()
        {
                

            if (_sdg != null) {
                var isMExists = (from ru in _sdg.U_EXTRA_REQUEST_USER
                                 from rdu in ru.U_EXTRA_REQUEST.U_EXTRA_REQUEST_DATA_USER
                                 where rdu.U_STATUS != "X" && rdu.U_REQ_TYPE == "M" 
                                 && ru.U_SDG_ID == _sdg.SDG_ID
                                 select rdu).Any();
                return isMExists;
            }
            return false;
            
        }
        void setbtnColor(bool change)
        {
            Color color;
            if (change)
            {
                color = Color.GreenYellow;
            }
            else
            {
                color = Color.FromArgb(232, 241, 252);
                ;

            }


            ((FillPrimitive)radButtonExtraMaterial.GetChildAt(0).GetChildAt(0)).BackColor =
                color;
            ((FillPrimitive)radButtonExtraMaterial.GetChildAt(0).GetChildAt(0)).BackColor2 =
                color;
            ((FillPrimitive)
                radButtonExtraMaterial.GetChildAt(0).GetChildAt(0)).BackColor3 = color;
            ((FillPrimitive)
                radButtonExtraMaterial.GetChildAt(0).GetChildAt(0)).BackColor4 = color;
            ((FillPrimitive)radButtonExtraMaterial.GetChildAt(0).GetChildAt(0)).BackColor =
                color;
        }
        //my code (AE)
        public void PaintBtn()//
        {
                setbtnColor(HasMaterialAddingReq());            
        }
        public void init(DataLayer dal, SDG sdg, INautilusServiceProvider sp, INautilusUser ntlsUser, long sid)
        {
      
            this._dal = dal;
            this._sdg = sdg;
            this._sp = sp;
            this.sid = sid;
            this._ntlsUser = Patholab_Common.Utils.GetNautilusUser(sp);   
         //   this.ExMaterialButton = radButtonExtraMaterial;

            //my code (AE)
            PaintBtn();
            
        }
        
        private void radButtonExtraMaterial_Click(object sender, EventArgs e)
        {
            if (_sdg != null)
            {
                using (ExtraMaterialForm form = new ExtraMaterialForm(_dal, _sdg, _sp, _ntlsUser, sid))
                {
                    form.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Button wasn't initialize via init() method.");
            }
        }
    }
}
