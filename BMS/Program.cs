using BMS.MQTT;
using BMS.Service;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("bms");
string connectionString_clickhouse = builder.Configuration.GetConnectionString("bms_clickhouse");
//MQTTHelper.SetConnectionString(connectionString);
MQTTHelperClickHouse.SetConnectionString(connectionString_clickhouse);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(
        CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(option => {
            option.LoginPath = "/Access/Login";
            option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        });
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IProjectManagementService, ProjectManagementService>();
builder.Services.AddScoped<ICustomerManagementService, CustomerManagementService>();
builder.Services.AddScoped<IDeviceManagementService, DeviceManagementService>();
builder.Services.AddScoped<IGroupManagementService, GroupManagementService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddHostedService<MqttSubscribeService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Access}/{action=Login}/{id?}");

app.Run();
