using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Final_Project_Data
{

    #region Application

    [MetadataType(typeof(ApplicationMetadata))]
    public partial class Application { }
    public class ApplicationMetadata
    {
        [Display(Name ="Application Date")]
        [DataType(DataType.Date,ErrorMessage ="Enter valid date")]
        [Required(ErrorMessage ="***Required***")]
        [DisplayFormat(ApplyFormatInEditMode =true, DataFormatString ="{0:d}", NullDisplayText ="[--N/A--]")]
        public System.DateTime ApplicationDate { get; set; }

        [StringLength(2000,ErrorMessage ="Manager notes must be 2000 characters or less")]
        [Display(Name ="Notes")]
        [UIHint("MultilineText")]
        public string ManagerNotes { get; set; }

        [StringLength(75,ErrorMessage ="File must be 75 or less characters")]
        [Required(ErrorMessage = "***Required***")]
        public string ResumeFilename { get; set; }

    }

    #endregion

    #region Location

    [MetadataType(typeof(LocationMetadata))]
    public partial class Location { }
    public class LocationMetadata
    {
        [StringLength(10,ErrorMessage ="Store number can only be 10 characters")]
        [Required(ErrorMessage = "***Required***")]
        public string StoreNumber { get; set; }

        [StringLength(50,ErrorMessage ="City can not be more than 50 chararcters")]
        [Required(ErrorMessage = "***Required***")]
        public string City { get; set; }

        [StringLength(2,ErrorMessage ="Use 2 letter state")]
        [Required(ErrorMessage = "***Required***")]
        public string State { get; set; }

        [StringLength(128,ErrorMessage ="Manager Id can not be more than 128 characters")]
        [Required(ErrorMessage = "***Required***")]
        [Display(Name ="Manager Id")]
        public string ManagerId { get; set; }

    }

    #endregion

    #region Position

    [MetadataType(typeof(PositionMetadata))]
    public partial class Position { }
    public class PositionMetadata
    {
        [StringLength(50,ErrorMessage ="Title can not be more than 50 characters")]
        [Required(ErrorMessage = "***Required***")]
        public string Title { get; set; }

        [UIHint("MultilineText")]
        public string JobDescription { get; set; }

    }

    #endregion

    #region ApplicationStatus

    [MetadataType(typeof(ApplicationStatusMetadata))]
    public partial class ApplicationStatus { }
    public class ApplicationStatusMetadata
    {
        [Display(Name = "Status Name")]
        [StringLength(50, ErrorMessage = " Status name can not be more than 50 characters")]
        [Required(ErrorMessage = "***Required***")]
        public string StatusName { get; set; }

        [UIHint("MultilineText")]
        [StringLength(250, ErrorMessage = "Status description can not be more than 250 characters")]
        [Display(Name ="Status Description")]
        public string StatusDescription { get; set; }

    }

    #endregion




}
