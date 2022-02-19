using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace UKLicense.Utilities
{
    public static class StaticSettings
    {
        public static bool SaveWithoutScanningDocument
        {
            get { return true; }
        }

        //NewRequest
        public static string NewRetirementRquestFolder
        {
            get { return "NewRequest"; }
        }

        public static string RetirementRquestRootFolder
        {
            get { return "RetirementRequest"; }
        }

        public static string NewRequestUploadPath
        {
            get { return "/Clients/Aiico/NewRequest"; }
        }

        public static string IncompleteDocumentLetterTemplateKey
        {
            get { return "incomplete.document"; }
        }

        public static string PassPortFolder
        {
            get { return @"C:\\ScannedPassport\\"; }
        }

        public static string DocumentsFolder
        {
            get { return @"C:\\ScannedDocs\\"; }
        }

        public static Guid RetirementBenefit
        {
            get { return Guid.Parse("C07F1AC1-4ED3-494E-9BDE-468EE94DD856"); }
        }

        public static Guid NSITF
        {
            get { return Guid.Parse("C3D6420C-C60D-4946-A74C-447B9BE52E56"); }
        }

        public static Guid SmallBalanceAndForeignerEbloc
        {
            get { return Guid.Parse("1481293d-fffa-4ca6-8a39-f7c3f197095a"); }
        }

        public static Guid LossEmployment
        {
            get { return Guid.Parse("C13E7262-8F2B-425F-8A39-4540B154ED6B"); }
        }

        public static Guid LegacyBenefit
        {
            get { return Guid.Parse("F198FE98-F979-46FF-B70D-42C599EC9F40"); }
        }

        public static Guid DeathBenefit
        {
            get { return Guid.Parse("EB834E4D-0FD7-4DB1-8475-A258642F3DC7"); }
        }

        public static Guid GratuityPension
        {
            get { return Guid.Parse("FFB2DF89-E9A1-4C76-B996-91F7E011915F"); }
        }

        public static Guid Annuity
        {
            get { return Guid.Parse("61F38D3A-85E0-45AE-893A-216A7BE1E622"); }
        }

        public static Guid VoluntaryContribution
        {
            get { return Guid.Parse("CD862146-9F18-4101-BBD8-1453E50664CF"); }
        }
        public static Guid MissingPerson
        {
            get { return Guid.Parse("AB27EA3F-F9C5-40DC-8A15-69E7A66C7687"); }
        } 

        public static Guid PermanentDisability
        {
            get { return Guid.Parse("62ABD384-5CCA-4200-83F3-22757BBEF036"); }
        }
        public static Guid PreAct
        {
            get { return Guid.Parse("1C7288D4-2341-4F85-8E49-5ACC068C9759"); }
        }

        public static String PFAKey
        {
            get { return "0001234"; }
        }

        //public static string BranchName { get { return "Ibadan"; } }

        public static string FrontCameraName { get; set; }

        public static string BackCameraName { get; set; }

        public static Guid ChangeOfNameDocument
        {
            get { return Guid.Parse("8eba7cb8-7f1d-4586-ba82-0d8b1973fd57"); }
        }

        public static Guid ChangeOfDateOfBirthDocument
        {
            get { return Guid.Parse("7215ed9f-c081-4c9c-b8d8-a1f89bab2ae7"); }
        }

       
        public static string Excel03ConString
        {
            get { return "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;HDR=Yes'"; }
        }

        public static string Excel07ConString
        {
            get { return "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties='Excel 8.0;HDR=Yes'"; }
        }
    }
}