﻿//// 
//// RunPackageScriptsAction.cs
//// 
//// Author:
////   Matt Ward <ward.matt@gmail.com>
//// 
//// Copyright (C) 2011-2014 Matthew Ward
//// 
//// Permission is hereby granted, free of charge, to any person obtaining
//// a copy of this software and associated documentation files (the
//// "Software"), to deal in the Software without restriction, including
//// without limitation the rights to use, copy, modify, merge, publish,
//// distribute, sublicense, and/or sell copies of the Software, and to
//// permit persons to whom the Software is furnished to do so, subject to
//// the following conditions:
//// 
//// The above copyright notice and this permission notice shall be
//// included in all copies or substantial portions of the Software.
//// 
//// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
//// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
//// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
//// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
//// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
////
//
//using System;
//using MonoDevelop.PackageManagement;
//using NuGet;
//
//namespace ICSharpCode.PackageManagement.Scripting
//{
//	internal class RunPackageScriptsAction : IDisposable
//	{
//		IPackageManagementProject2 project;
//		IPackageScriptFactory scriptFactory;
//		IPackageScriptRunner scriptRunner;
//		ExtendedPackageManagementProject extendedProject;
//		IGlobalMSBuildProjectCollection projectCollection;
//		
//		public RunPackageScriptsAction(
//			IPackageScriptRunner scriptRunner,
//			IPackageManagementProject2 project)
//			: this(project, scriptRunner, new PackageScriptFactory (), new GlobalMSBuildProjectCollection ())
//		{
//		}
//		
//		public RunPackageScriptsAction (
//			IPackageManagementProject2 project,
//			IPackageScriptRunner scriptRunner,
//			IPackageScriptFactory scriptFactory,
//			IGlobalMSBuildProjectCollection projectCollection)
//		{
//			this.project = project;
//			extendedProject = project as ExtendedPackageManagementProject;
//			this.scriptRunner = scriptRunner;
//			this.scriptFactory = scriptFactory;
//			this.projectCollection = projectCollection;
//			
//			projectCollection.AddProject(project);
//			RegisterEvents();
//		}
//		
//		void RegisterEvents()
//		{
//			project.PackageInstalled += PackageInstalled;
//			project.PackageReferenceAdded += PackageReferenceAdded;
//
//			if (extendedProject != null) {
//				extendedProject.ProjectManager.PackageReferenceRemoving += PackageReferenceRemoving;
//			}
//		}
//		
//		void UnregisterEvents()
//		{
//			project.PackageInstalled -= PackageInstalled;
//			project.PackageReferenceAdded -= PackageReferenceAdded;
//			if (extendedProject != null) {
//				extendedProject.ProjectManager.PackageReferenceRemoving -= PackageReferenceRemoving;
//			}
//		}
//		
//		void PackageInstalled(object sender, PackageOperationEventArgs e)
//		{
//			RunInitScript(e);
//		}
//		
//		void PackageReferenceRemoving(object sender, PackageOperationEventArgs e)
//		{
//			RunUninstallScript(e);
//		}
//		
//		void PackageReferenceAdded(object sender, PackageOperationEventArgs e)
//		{
//			RunInstallScript(e);
//		}
//		
//		void RunInitScript(PackageOperationEventArgs e)
//		{
//			IPackageScript script = scriptFactory.CreatePackageInitializeScript(e.Package, e.InstallPath);
//			RunScript(script);
//		}
//		
//		void RunScript(IPackageScript script)
//		{
//			script.Project = project;
//			scriptRunner.Run(script);
//		}
//		
//		void RunUninstallScript(PackageOperationEventArgs e)
//		{
//			IPackageScript script = scriptFactory.CreatePackageUninstallScript(e.Package, e.InstallPath);
//			RunScript(script);
//		}
//		
//		void RunInstallScript(PackageOperationEventArgs e)
//		{
//			IPackageScript script = scriptFactory.CreatePackageInstallScript(e.Package, e.InstallPath);
//			RunScript(script);
//		}
//		
//		public void Dispose()
//		{
//			IsDisposed = true;
//			UnregisterEvents();
//			projectCollection.Dispose();
//		}
//		
//		public bool IsDisposed { get; private set; }
//	}
//}
