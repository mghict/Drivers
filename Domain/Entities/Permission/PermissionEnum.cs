using System.ComponentModel;

namespace Driver.Domain.Entities;

public enum PermissionEnum
{

    // Mine Managment
    [Description("نمایش معادن")]
    MineView = 200,

    [Description("ایجاد معادن")]
    MineCreate = 201,

    [Description("ویرایش معادن")]
    MineEdit = 202,

    [Description("حذف معادن")]
    MineDelete = 203,


    // AutoBrand Managment
    [Description("نمایش برند خودرو")]
    AutoBrandView = 210,

    [Description("ایجاد برند خودرو")]
    AutoBrandCreate = 211,

    [Description("ویرایش برند خودرو")]
    AutoBrandEdit = 212,

    [Description("حذف برند خودرو")]
    AutoBrandDelete = 213,



    // AutoModel Managment
    [Description("نمایش مدل خودرو")]
    AutoModelView = 220,

    [Description("ایجاد مدل خودرو")]
    AutoModelCreate = 221,

    [Description("ویرایش مدل خودرو")]
    AutoModelEdit = 222,

    [Description("حذف مدل خودرو")]
    AutoModelDelete = 223,


    // Auto Managment
    [Description("نمایش خودرو")]
    AutoView = 230,

    [Description("ایجاد خودرو")]
    AutoCreate = 231,

    [Description("ویرایش خودرو")]
    AutoEdit = 232,

    [Description("حذف خودرو")]
    AutoDelete = 233,


    // Driver Managment
    [Description("نمایش رانندگان")]
    DriverView = 240,

    [Description("ایجاد رانندگان")]
    DriverCreate = 241,

    [Description("ویرایش رانندگان")]
    DriverEdit = 242,

    [Description("حذف رانندگان")]
    DriverDelete = 243,


    // Person Managment
    [Description("نمایش کاربران")]
    PersonView = 250,

    [Description("ایجاد کاربران")]
    PersonCreate = 251,

    [Description("ویرایش کاربران")]
    PersonEdit = 252,

    [Description("حذف کاربران")]
    PersonDelete = 253,

    // Province Managment
    [Description("نمایش استان")]
    ProvinceView = 260,

    [Description("ایجاد استان")]
    ProvinceCreate = 261,

    [Description("ویرایش استان")]
    ProvinceEdit = 262,

    [Description("حذف استان")]
    ProvinceDelete = 263,

    // City Managment
    [Description("نمایش شهر")]
    CityView = 264,

    [Description("ایجاد شهر")]
    CityCreate = 265,

    [Description("ویرایش شهر")]
    CityEdit = 266,

    [Description("حذف شهر")]
    CityDelete = 267,


    // Material Managment
    [Description("نمایش مواد معدنی")]
    MaterialView = 270,

    [Description("ایجاد مواد معدنی")]
    MaterialCreate = 271,

    [Description("ویرایش مواد معدنی")]
    MaterialEdit = 272,

    [Description("حذف مواد معدنی")]
    MaterialDelete = 273,

    // Reports Managment
    [Description("گزارش تردد خودرو")]
    AutoHistoryReportView = 300,

    [Description("گزارش خلاصه عملکرد استان")]
    ProvinceReportView = 301,

    [Description("گزارش خلاصه عملکرد معدن")]
    MineReportView = 302,

    [Description("گزارش وضعیت خودرو")]
    AutoStatusReportView = 303,

    [Description("گزارش خودرو غیرفعال")]
    AutoOffDeviceReportView = 304,
}
