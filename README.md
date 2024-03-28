# Content Cleaner

An Optimizely CMS 12 plugin to let you find content of a specific type and delete all or selected instances.

## Description

Occasionally, there arises the necessity to eliminate content corresponding to specific types. This need may arise, for instance, during content cleanup procedures before transitioning to a new environment for User Acceptance Testing (UAT), or when there is a desire to discard certain content types altogether.

Typically, content of particular types is scattered throughout the system, particularly with multimedia assets and blocks, rendering the process quite laborious. Consequently, I devised a compact administrative mode plugin that facilitates the selection of a type and provides a comprehensive list of all content instances associated with that type. It further enables the deletion of either all instances or selectively chosen ones of that particular type.

## Getting Started

### Installing

* Ensure you have a source configured for the [Optimizely nuget feed](https://nuget.optimizely.com/feed/)
* Locate the package 'Content Cleaner' from the optimizely feed and install.
* Configure 'Content Cleaner' in the Startup.cs in the ConfigureServices method, below is an example :
```csharp

   public void ConfigureServices(IServiceCollection services)
 {
     if (_webHostingEnvironment.IsDevelopment())
     {
         AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(_webHostingEnvironment.ContentRootPath, "App_Data"));

         services.Configure<SchedulerOptions>(options => options.Enabled = false);
     }

     services
         .AddCmsAspNetIdentity<ApplicationUser>()
         .AddCms()
         .AddAlloy()
         .AddAdminUserRegistration()
         .AddEmbeddedLocalization<Startup>();

     // Required by Wangkanai.Detection
     services.AddDetection();

     services.AddSession(options =>
     {
         options.IdleTimeout = TimeSpan.FromSeconds(10);
         options.Cookie.HttpOnly = true;
         options.Cookie.IsEssential = true;
     });

     services.AddContentCleaner();
 }

```
* Configure the application to ensure that controllers are mapped and routed also by updating your application configuration, below is an example:
```csharp

public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    // Required by Wangkanai.Detection
    app.UseDetection();
    app.UseSession();

    app.UseStaticFiles();
    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();
    app.AddContentCleaner();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapContent();
    });
}

```

### Executing program

* Start the application and login in tothe CMS.
* Locate 'Content Cleaner' under 'Add-ons' from the navigation panel
  ![Addo-On Content Cleaner](https://github.com/paulmcgann/content-cleaner/assets/12101226/d4b860f8-c902-4b79-908c-17f9d96f4d15)
* Select a 'Content Type' from the dropdown.
  ![Content Cleaner - Content Type Selection](https://github.com/paulmcgann/content-cleaner/assets/12101226/2a680648-d88d-4576-ac0f-a3364f302755)
* A list of content will appear that is using the content type.
  ![content Cleaner - List of content for content type](https://github.com/paulmcgann/content-cleaner/assets/12101226/8a147f14-008f-4582-b7c4-ccb4147d7946)
* Click the delete button and the content will be deleted.
