using System;
using System.Collections.Generic;
using System.Linq;
using Nudo.Engine.Logging;
using Spark;

namespace Nudo.Engine.Builder
{
    public abstract class BuilderBase<TModel> : SparkViewBase, IBuilder
    {
        private readonly IDictionary<string, Target> _targets = new Dictionary<string, Target>();

        public IDictionary<string, Target> Targets
        {
            get { return _targets; }
        }

        public string DefaultTarget { get; set; }

        public void RegisterTarget(string name, string dependencies, string description, Action method)
        {
            if (string.IsNullOrEmpty(DefaultTarget))
            {
                DefaultTarget = name;
            }

            Target target;
            if (!Targets.TryGetValue(name, out target))
            {
                target = new Target { Name = name, Dependencies = new List<string>() };
                Targets.Add(name, target);
            }

            if (!string.IsNullOrWhiteSpace(dependencies))
            {
                target.Dependencies = target.Dependencies
                    .Concat(dependencies.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries)).
                    ToList();
            }

            if (!string.IsNullOrWhiteSpace(description))
            {
                target.Description = description;
            }

            if (method != null)
            {
                target.Method = method;
            }
        }

        public void StartingTarget(string name)
        {
            foreach (var dependency in Targets[name].Dependencies)
            {
                CallTarget(dependency);
            }
            Log.Info(string.Format("Target \x1b-\x0f{0}\x1b-\x07", name));
        }

        public void CallTarget(string name)
        {
            Targets[name].Method.Invoke();
        }

        public void Echo(object value)
        {
            Log.Info(value);
        }

        protected object HTML(object value)
        {
            return value;
        }

        protected object H(object value)
        {
            Log.Info(value);
            return "";
        }

        public ILog Log { get; set; }
    }

    public abstract class BuilderBase : BuilderBase<object>
    {

    }
}
