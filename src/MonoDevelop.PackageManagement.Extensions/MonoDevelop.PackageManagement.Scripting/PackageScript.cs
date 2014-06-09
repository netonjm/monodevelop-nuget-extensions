﻿// 
// PackageScript.cs
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

using System;
using ICSharpCode.PackageManagement.EnvDTE;
using NuGet;

namespace ICSharpCode.PackageManagement.Scripting
{
	public class PackageScript : IPackageScript
	{
		public PackageScript(IPackage package, IPackageScriptFileName fileName)
		{
			this.Package = package;
			this.ScriptFileName = fileName;
		}
		
		protected IPackageScriptFileName ScriptFileName { get; private set; }
		protected IPackageScriptSession Session { get; private set; }
		
		public IPackage Package { get; set; }
		public IPackageManagementProject Project { get; set; }
		
		public bool Exists()
		{
			return ScriptFileName.FileExists();
		}
		
		public void Run(IPackageScriptSession session)
		{
			this.Session = session;
			Run();
		}
		
		void Run()
		{
			BeforeRun();
			if (Exists()) {
				AddSessionVariables();
				RunScript();
				RemoveSessionVariables();
			}
		}
		
		protected virtual void BeforeRun()
		{
		}
		
		void AddSessionVariables()
		{
			Session.AddVariable("__rootPath", ScriptFileName.PackageInstallDirectory);
			Session.AddVariable("__toolsPath", ScriptFileName.GetScriptDirectory());
			Session.AddVariable("__package", Package);
			//Session.AddVariable("__project", GetProject());
		}
		
//		Project GetProject()
//		{
//			if (Project != null) {
//				return Project.ConvertToDTEProject();
//			}
//			return null;
//		}
		
		void RunScript()
		{
			string script = GetScript();
			Session.InvokeScript(script);
		}
		
		string GetScript()
		{
			return String.Format(
				"& '{0}' $__rootPath $__toolsPath $__package $__project",
				ScriptFileName);
		}
		
		void RemoveSessionVariables()
		{
			Session.RemoveVariable("__rootPath");
			Session.RemoveVariable("__toolsPath");
			Session.RemoveVariable("__package");
			Session.RemoveVariable("__project");
		}
	}
}
