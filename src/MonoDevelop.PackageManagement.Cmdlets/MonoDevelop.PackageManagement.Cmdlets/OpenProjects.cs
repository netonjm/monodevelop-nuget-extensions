﻿// 
// OpenProjects.cs
// 
// Author:
//   Matt Ward <ward.matt@gmail.com>
// 
// Copyright (C) 2011-2014 Matthew Ward
// 
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System.Collections.Generic;
using System.Management.Automation;

using DTEProject = ICSharpCode.PackageManagement.EnvDTE.Project;
using MonoDevelop.Projects;
using System.Linq;

namespace ICSharpCode.PackageManagement.Cmdlets
{
	internal class OpenProjects
	{
		Solution solution;

		public OpenProjects (Solution solution)
		{
			this.solution = solution;
		}
		
		public IEnumerable<EnvDTE.Project> GetAllProjects ()
		{
			foreach (Project project in solution.GetAllProjects ().OfType<DotNetProject> ()) {
				yield return CreateProject (project);
			}
		}
		
		DTEProject CreateProject (Project project)
		{
			return new DTEProject (project as DotNetProject);
		}
		
		public IEnumerable<EnvDTE.Project> GetFilteredProjects (string[] projectNames)
		{
			foreach (string projectName in projectNames) {
				WildcardPattern wildcard = CreateWildcard (projectName);
				foreach (EnvDTE.Project project in GetAllProjects ()) {
					if (wildcard.IsMatch (project.Name)) {
						yield return project;
					}
				}
			}
		}
		
		WildcardPattern CreateWildcard (string pattern)
		{
			return new WildcardPattern (pattern, WildcardOptions.IgnoreCase);
		}
	}
}
