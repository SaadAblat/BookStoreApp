using BookStore.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public RegisterModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager
            , RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        [BindProperty]
        public InputModel Input { get; set; }
        public string ReturnUrl { get; set; }



        public void OnGet()
        {
            ReturnUrl = Url.Content("~/");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ReturnUrl = Url.Content("~/");

            if (ModelState.IsValid)
            {
                var userExists = await _userManager.FindByNameAsync(Input.Name);
                if (userExists != null)
                {
                    ModelState.AddModelError(string.Empty, "This user name is already taken. Try another one.");
                    return Page();
                }
                var identity = new ApplicationUser { UserName = Input.Name, RegistrationDate=DateTime.Now };
                var result = await _userManager.CreateAsync(identity, Input.Password);

                // Roles
                var role = new IdentityRole(Input.Role);

                var roleExists = await _roleManager.FindByNameAsync(role.Name);
                if (roleExists == null)
                {
                    var addRoleResults = await _roleManager.CreateAsync(role);
                    if (!addRoleResults.Succeeded)
                    {
                        return Page();
                    }
                }
                
                var addUserRoleResult = await _userManager.AddToRoleAsync(identity, Input.Role);
                if (result.Succeeded && addUserRoleResult.Succeeded)
                {
                    await _signInManager.SignInAsync(identity, isPersistent: false);
                    return LocalRedirect(ReturnUrl);
                }
            }
            return Page();
        }

        public class InputModel
        {
            [Required]
            public string Name { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Required]
            public string Role { get; set; }

        }
    }


}
