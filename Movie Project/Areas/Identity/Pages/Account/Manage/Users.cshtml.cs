using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models;
using Utility;

namespace Movie_Project.Areas.Identity.Pages.Account.Manage
{
	[Authorize(Roles = "Admin")]
	public class UsersModel : PageModel
    {
		//private readonly UserManager<IdentityUser> _userManager;
		//private readonly RoleManager<IdentityRole> _roleManager;


		//      public UsersModel(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager )
		//      {
		//          _roleManager = roleManager;
		//          _userManager = userManager;

		//}

		//// ViewModel to hold user and their associated roles
		//public class UserWithRoles
		//{
		//	public IdentityUser User { get; set; }
		//	public IList<string> Roles { get; set; }
		//}

		//// Property to hold the list of users with their roles
		//public List<UserWithRoles> UsersWithRolesList { get; set; }

		////--------------------------------------------------------------------------------------

		//public List<IdentityUser> userList { get; set; }

		//      public List<IdentityRole> roleList { get; set; }

		//      public void OnGet()
		//      {
		//          userList = _userManager.Users.ToList();
		//          roleList = _roleManager.Roles.ToList();



		//      }





		private readonly UserManager<IdentityUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public UsersModel(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			_roleManager = roleManager;
			_userManager = userManager;
		}

		// ViewModel to hold user and their associated roles
		public class UserWithRoles
		{
			public IdentityUser User { get; set; }
			public IList<string> Roles { get; set; }
		}

		// Property to hold the list of users with their roles
		public List<UserWithRoles> UsersWithRolesList { get; set; }

		public List<IdentityRole> roleList { get; set; }

		public async Task OnGetAsync()
		{
			// Initialize the list
			UsersWithRolesList = new List<UserWithRoles>();
			roleList = _roleManager.Roles.ToList();

			// Get all users
			var users = _userManager.Users.ToList();

			foreach (var user in users)
			{
				// Get roles for the user
				var roles = await _userManager.GetRolesAsync(user);

				// Add user and roles to the list
				UsersWithRolesList.Add(new UserWithRoles
				{
					User = user,
					Roles = roles
				});
			}
		}


		// --------------------------Delete --------------------------------------

		// Method to handle deleting a user
		public async Task<IActionResult> OnPostDeleteAsync(string userId)
		{
			var user = await _userManager.FindByIdAsync(userId);
			if (user != null)
			{
				var result = await _userManager.DeleteAsync(user);
				if (result.Succeeded)
				{
					return RedirectToPage();
				}
				// Handle error scenario
			}
			return RedirectToPage();
		}

		//----------------------EDIT---------------------------------------------------------------

		// Method to handle editing a user (updating roles)
		public async Task<IActionResult> OnPostEditAsync(string userId, List<string> roles)
		{
			var user = await _userManager.FindByIdAsync(userId);
			if (user != null)
			{
				// Remove user from all current roles
				var currentRoles = await _userManager.GetRolesAsync(user);
				await _userManager.RemoveFromRolesAsync(user, currentRoles);

				// Assign selected roles to the user
				await _userManager.AddToRolesAsync(user, roles);

				return RedirectToPage();
			}
			// Handle error scenario
			return RedirectToPage();
		}


	}
}
