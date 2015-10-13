
using System.IO;
using System.Xml.Serialization;
using System;
using System.Windows.Forms;
using System.Collections.Generic;


namespace Import_Export_Universel
{
    public class ImportExportException : Exception
    {
        private Exception exceptionSource;
        private string strOrigine;

        public ImportExportException( string _strOrigine, string _strMessage, Exception _exceptionSource ) : base(_strMessage)
        {
            this.exceptionSource = _exceptionSource;
            this.strOrigine = _strOrigine;
        }

        public string Origine
        {
            get
            {
                return this.strOrigine;
            }
        }

        public Exception ExceptionSource
        {
            get
            {
                return this.exceptionSource;
            }
        }

        public override string Message
        {
            get
            {
            	if( this.exceptionSource != null )
	                return 
	                    "[" + this.strOrigine + "/" + this.exceptionSource.GetType().ToString() + 
	                    "] : " + base.Message + "\n\n" + this.exceptionSource.Message;
            	else
	                return 
	                    "[" + this.strOrigine + 
	                    "] : " + base.Message ;
            }
        }

        public override string ToString()
        {
            return this.Message;
            //return base.ToString();
        }
    }
}
