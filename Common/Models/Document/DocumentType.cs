using System.ComponentModel;

namespace Driver.Common.Models;

/// <summary>
/// نوع سند 
/// </summary>
public enum DocumentType
{

    [Description("تصویر راننده")]
    DriverPicture = 0,

    [Description("تصویر ماشین")]
    AutoPicture = 1,

}
