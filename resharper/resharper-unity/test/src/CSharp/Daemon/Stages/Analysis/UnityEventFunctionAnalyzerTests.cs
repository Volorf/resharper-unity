﻿using JetBrains.ReSharper.Plugins.Unity.CSharp.Daemon.Stages.Highlightings;
using NUnit.Framework;

namespace JetBrains.ReSharper.Plugins.Unity.Tests.CSharp.Daemon.Stages.Analysis
{
    [TestUnity]
    public class UnityEventFunctionAnalyzerTests : CSharpHighlightingTestWitProductDependentGoldBase<IUnityHighlighting>
    {
        protected override string RelativeTestDataRoot => @"CSharp\Daemon\Stages\Analysis";

        [Test] public void TestUnityEventFunctionAnalyzer() { DoNamedTest2(); }
    }
}