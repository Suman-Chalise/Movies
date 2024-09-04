using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Movie_Project.Areas.Identity.Pages.Account.Manage
{
    [Authorize(Roles = "Admin")]
    public class ManageRolesModel : PageModel
    {
      

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ManageRolesModel(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IList<IdentityUser> Users { get; set; }
        public IList<IdentityRole> Roles { get; set; }

        [BindProperty]
        public string SelectedUserId { get; set; }

        [BindProperty]
        //public IList<string> SelectedRoles { get; set; }

        public string SelectedRoles { get; set; }

        public async Task OnGetAsync()
        {
            Users = _userManager.Users.ToList();
            Roles = _roleManager.Roles.ToList();
        }

         public async Task<IActionResult> OnPostAsync()
  
        {
            if (string.IsNullOrEmpty(SelectedUserId) || SelectedRoles == null)
            {
                return Page();
            }

            var user = await _userManager.FindByIdAsync(SelectedUserId);
           
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{SelectedUserId}'.");
            }
            
           
            var userRoles = await _userManager.GetRolesAsync(user);

            var removeRolesResult = await _userManager.RemoveFromRolesAsync(user, userRoles);

            if (!removeRolesResult.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Failed to remove existing roles.");
                return Page();
            }

            //var addRolesResult = await _userManager.AddToRolesAsync(user, SelectedRoles);

            //if (!addRolesResult.Succeeded)
            //{
            //    ModelState.AddModelError(string.Empty, "Failed to assign selected roles.");
            //    return Page();
            //}

            return RedirectToPage();
        }
    }
}
