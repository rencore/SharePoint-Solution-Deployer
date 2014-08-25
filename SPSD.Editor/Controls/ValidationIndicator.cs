#region

using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using SPSD.Editor.Utilities;

#endregion

namespace SPSD.Editor.Controls
{
    public partial class ValidationIndicator : UserControl
    {
        private static readonly Regex RegexNoSpecChars = new Regex(@"^[a-zA-Z0-9]*$");

        private static readonly string[] ErrorMessages = new[]
            {
                "This field cannot be empty.",
                "The value of this field cannot contain any whitespaces.",
                "The value of this field cannot contain any special characters.",
                "The value of this field must be unique.",
                "The value of this field must be a number.",
                "The value of this field must be a positive number.",
                "The value of this field cannot start with a number."
            };

        private Control _controlToValidate;

        public ValidationIndicator()
        {
            InitializeComponent();
        }

        [Category("Validation Indicator")]
        public Control ControlToValidate
        {
            get { return _controlToValidate; }
            set
            {
                _controlToValidate = value;
                if (_controlToValidate != null)
                {
                    _controlToValidate.TextChanged += ControlToValidate_Validate;
                    _controlToValidate.EnabledChanged += ControlToValidate_Validate;
                    //_controlToValidate.GotFocus += ControlToValidate_Validate;
                    //_controlToValidate.LostFocus += ControlToValidate_Validate;
                    _controlToValidate.KeyPress += ControlToValidate_Validate;
                    //_controlToValidate.Validating += ControlToValidate_Validate;
                }
            }
        }

        [Category("Validation Indicator")]
        public bool IsValid { get; set; }

        [Category("Validation Indicator")]
        public bool CannotBeEmpty { get; set; }

        [Category("Validation Indicator")]
        public bool NoWhiteSpaces { get; set; }

        [Category("Validation Indicator")]
        public bool NoSpecialChars { get; set; }

        [Category("Validation Indicator")]
        public bool MustBeUnique { get; set; }

        [Category("Validation Indicator")]
        public bool MustBeInt { get; set; }

        [Category("Validation Indicator")]
        public bool MustBePostitiveInt { get; set; }

        [Category("Validation Indicator")]
        public bool CannotStartWithInteger { get; set; }

        [Category("Validation Indicator")]
        public string DefaultValue { get; set; }

        [Category("Validation Indicator")]
        [Description(
            "Value which is considered as the information text which is shown in the field when nothing is entered, the value is treated like the field is empty"
            )]
        public string InfoValue { get; set; }

        [Category("Validation Indicator")]
        [Description(
            "The text which will be used when the value is not valid when the GetValidatedValue method is called"
            )]
        public string SaveErrorFieldDesc { get; set; }

        private void ValidationIndicator_Load(object sender, EventArgs e)
        {
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Validate();
            base.OnPaint(e);
        }

        private void ControlToValidate_Validate(object sender, EventArgs e)
        {
            Validate();
        }

        public new void Validate()
        {
            if (_controlToValidate != null &&
                _controlToValidate.Parent.Parent != null &&
                _controlToValidate.Visible)
            {
                using (Graphics g = ControlToValidate.Parent.CreateGraphics())
                {
                    Rectangle rect = _controlToValidate.Bounds;
                    rect.Inflate(1, 1);

                    bool showWarning = _controlToValidate.Enabled && !ControlIsValid();
                    Visible = showWarning;
                    if (showWarning)
                    {
                        ControlPaint.DrawBorder(g, rect, Color.Red, ButtonBorderStyle.Solid);
                        return;
                    }
                    ControlPaint.DrawBorder(g, rect, SystemColors.Control, ButtonBorderStyle.Solid);
                }
            }
            else
            {
                IsValid = true;
                Visible = false;
            }
        }

        private bool ControlIsValid()
        {
            StringBuilder message = new StringBuilder();
            if (ControlToValidate != null)
            {
                string value = ControlToValidate.Text;
                if (CannotBeEmpty &&
                    (string.IsNullOrEmpty(value) || value.Equals(InfoValue)))
                {
                    message.AppendLine(ErrorMessages[0]);
                }
                if (!string.IsNullOrEmpty(value))
                {
                    if (NoWhiteSpaces &&
                        Regex.Replace(value, @"\s", "").Length != value.Length)
                    {
                        message.AppendLine(ErrorMessages[1]);
                    }
                    if (NoSpecialChars && !RegexNoSpecChars.IsMatch(value))
                    {
                        message.AppendLine(ErrorMessages[2]);
                    }
                    if (MustBeUnique)
                    {
                        message.AppendLine(ErrorMessages[3]);
                    }
                    long longval;
                    bool isNumber = Int64.TryParse(value, out longval);
                    if (MustBeInt && !isNumber)
                    {
                        message.AppendLine(ErrorMessages[4]);
                    }
                    if (MustBePostitiveInt && (!isNumber || longval < 0))
                    {
                        message.AppendLine(ErrorMessages[5]);
                    }
                    isNumber = Int64.TryParse(value[0].ToString(), out longval);
                    if (CannotStartWithInteger && isNumber)
                    {
                        message.AppendLine(ErrorMessages[6]);
                    }
                }
                if (string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(DefaultValue))
                {
                    message.AppendLine(string.Format("The default value is: {0}", DefaultValue));
                }
                toolTip.SetToolTip(this, message.ToString());
            }
            IsValid = message.Length <= 0;
            return IsValid;
        }

        public string GetValidatedValue()
        {
            Validate();
            if (!IsValid)
            {
                EnvironmentFileHandler.AddNotification(string.Format("The {0} has still invalid or missing values.",
                                                                     SaveErrorFieldDesc));
                return "";
                //throw new InvalidExpressionException(string.Format("The {0} has still invalid or missing values.",
                //                                                   SaveErrorFieldDesc));
            }
            return _controlToValidate.Text.Trim();
        }
    }
}