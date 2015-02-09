﻿using System;
using System.ComponentModel.Composition;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.Controls;
using Microsoft.VisualStudio.Shell;
using GitHub.Api;

namespace GitHub.VisualStudio
{
    [TeamExplorerNavigationItem(IssuesNavigationItemId,
        NavigationItemPriority.Issues,
        TargetPageId = TeamExplorerPageIds.Home)]
    class IssuesNavigationItem : TeamExplorerNavigationItemBase
    {
        public const string IssuesNavigationItemId = "5245767A-B657-4F8E-BFEE-F04159F1DDA4";

        [ImportingConstructor]
        public IssuesNavigationItem([Import(typeof(SVsServiceProvider))] IServiceProvider serviceProvider,
            ISimpleApiClientFactory apiFactory)
            : base(serviceProvider, apiFactory)
        {
            Text = "Issues";
            IsVisible = false;
            IsEnabled = true;
            Image = Resources.issue_opened;

            UpdateState();
        }

        protected override void ContextChanged(object sender, ContextChangedEventArgs e)
        {
            UpdateState();
            base.ContextChanged(sender, e);
        }

        public override void Execute()
        {
            base.Execute();
        }

        protected override async void UpdateState()
        {
            base.UpdateState();

            if (IsVisible)
            {
                var repo = await SimpleApiClient.GetRepository();
                IsEnabled = repo != null && repo.HasIssues;
            }
        }
    }
}
