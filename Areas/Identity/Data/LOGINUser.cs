using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TMDT.Areas.Identity.Data;

// Add profile data for application users by adding properties to the LOGINUser class
public class LOGINUser : IdentityUser
{
	public string? Firstname { get; set; }
	public string? Lastname { get; set; }
}

