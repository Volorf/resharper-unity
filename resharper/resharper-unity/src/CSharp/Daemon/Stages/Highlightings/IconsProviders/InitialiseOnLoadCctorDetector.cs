using System.Collections.Generic;
using JetBrains.Application.Settings.Implementation;
using JetBrains.Application.UI.Controls.BulbMenu.Items;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Plugins.Unity.CSharp.Daemon.Stages.PerformanceCriticalCodeAnalysis.Analyzers;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util.Collections;

namespace JetBrains.ReSharper.Plugins.Unity.CSharp.Daemon.Stages.Highlightings.IconsProviders
{
    [SolutionComponent]
    public class InitialiseOnLoadCctorDetector : UnityDeclarationHighlightingProviderBase
    {
        
        public InitialiseOnLoadCctorDetector(ISolution solution, SolutionAnalysisService swa, SettingsStore settingsStore, 
            PerformanceCriticalCodeCallGraphAnalyzer analyzer)
            : base(solution, swa, settingsStore, analyzer)
        {
        }
        
        public override IDeclaredElement Analyze(IDeclaration node, IHighlightingConsumer consumer, DaemonProcessKind kind)
        {
            if (!(node is IConstructorDeclaration element))
                return null;
            
            if (!element.IsStatic)
                return null;

            var containingType = element.GetContainingTypeDeclaration()?.DeclaredElement;
            if (containingType != null &&
                containingType.HasAttributeInstance(KnownTypes.InitializeOnLoadAttribute, false))
            {
                AddHighlighting(consumer, element, "Implicit usage", 
                    "Called when Unity first launches the editor, the player, or recompiles scripts", kind);
                return containingType;
            }

            return null;
        }

        protected override IEnumerable<BulbMenuItem> GetActions(ICSharpDeclaration declaration)
        {
            return EnumerableCollection<BulbMenuItem>.Empty;
        }
    }
}