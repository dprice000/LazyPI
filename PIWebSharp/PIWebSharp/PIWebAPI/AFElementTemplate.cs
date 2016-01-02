using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIWebSharp.WebAPI
{
    public class AFElementTemplate : PIWebSharp.AFElementTemplate
    {
        private PIWebSharp.WebAPI.AFElementTemplate _template;


        public string WebID
        {
            get
            {
                return _template.WebID;
            }
        }

        public string Name
        {
            get
            {
                return _template.Name;
            }
            set
            {
                _template.Name = value;
            }
        }

        public string Description
        {
            get
            {
                return _template.Description;
            }
            set
            {
                _template.Description = value;
            }
        }
    }
}
