// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EnvDTE;
using NuGet.Frameworks;
using NuGet.ProjectManagement;
using Task = System.Threading.Tasks.Task;

namespace NuGet.PackageManagement.VisualStudio
{
    /// <summary>
    /// An interface implemented by an adapter for DTE calls, to allow simplified calls and mocking for tests.
    /// </summary>
    public interface IEnvDTEProjectAdapter
    {
        /// <summary>
        /// In unavoidable circumstances where we need to DTE object, it's exposed here
        /// </summary>
        Project DTEProject { get; }

        /// <summary>
        /// Project Name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Unique project name
        /// </summary>
        string UniqueName { get; }

        /// <summary>
        /// Full file path of project
        /// </summary>
        string ProjectFullPath { get; }

        /// <summary>
        /// Base intermediate path (e.g. c:\projFoo\obj)
        /// </summary>
        string BaseIntermediatePath { get; }

        /// <summary>
        /// Project supports extracting reference collections
        /// </summary>
        bool SupportsReferences { get; }

        /// <summary>
        /// Is this project a non-CPS package reference based csproj?
        /// </summary>
        bool IsLegacyCSProjPackageReferenceProject { get; }

        /// <summary>
        /// All dependency graph projects referenced by project
        /// </summary>
        IEnumerable<IDependencyGraphProject> ReferencedDependencyGraphProjects { get; }

        /// <summary>
        /// Project's target framework
        /// </summary>
        Task<NuGetFramework> GetTargetNuGetFramework();

        /// <summary>
        /// Project references for legacy CSProj project
        /// </summary>
        /// <param name="desiredMetadata">metadata element names requested in returned objects</param>
        /// <returns>An array of returned data for each project reference discovered</returns>
        Task<IEnumerable<LegacyCSProjProjectReference>> GetLegacyCSProjProjectReferencesAsync(Array desiredMetadata);

        /// <summary>
        /// Package references for legacy CSProj project
        /// </summary>
        /// <param name="desiredMetadata">metadata element names requested in returned objects</param>
        /// <returns>An array of returned data for each package reference discovered</returns>
        Task<IEnumerable<LegacyCSProjPackageReference>> GetLegacyCSProjPackageReferencesAsync(Array desiredMetadata);

        /// <summary>
        /// Add a new package reference or update an existing one on a legacy CSProj project
        /// </summary>
        /// <param name="packageName">Name of package to add or update</param>
        /// <param name="packageVersion">Version of new package/new version of existing package</param>
        /// <param name="metadataElements">Element names of metadata to add to package reference</param>
        /// <param name="metadataValues">Element values of metadata to add to package reference</param>
        Task AddOrUpdateLegacyCSProjPackageAsync(string packageName, string packageVersion, string[] metadataElements, string[] metadataValues);

        /// <summary>
        /// Remove a package reference from a legacy CSProj project
        /// </summary>
        /// <param name="packageName">Name of package to remove from project</param>
        Task RemoveLegacyCSProjPackageAsync(string packageName);
    }
}
