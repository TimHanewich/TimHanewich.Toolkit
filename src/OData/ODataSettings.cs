using System;

namespace TimHanewich.Toolkit.OData
{
    public class ODataSettings
    {
        public bool AllowMultiRowModification {get; set;}

        public ODataSettings()
        {
            AllowMultiRowModification = false;
        }
    }
}