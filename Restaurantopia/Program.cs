using Microsoft.EntityFrameworkCore;
using Restaurantopia.InterFaces;
using Restaurantopia.Models;
using Restaurantopia.Repositories;

namespace Restaurantopia
{
	public class Program
	{
		public static void Main ( string[] args )
		{
			var builder = WebApplication.CreateBuilder ( args );
			
			builder.Services.AddControllersWithViews ();
            builder.Services.AddDbContext<MyDbContext>(Opt => Opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IUploadFile, UploadFile>();
            var app = builder.Build ();

			
			if (!app.Environment.IsDevelopment ())
			{
				app.UseExceptionHandler ( "/Home/Error" );
				app.UseHsts ();
			}

			app.UseHttpsRedirection ();
			app.UseStaticFiles ();

			app.UseRouting ();

			app.UseAuthorization ();

			app.MapControllerRoute (
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}" );

			app.Run ();
		}
	}
}
