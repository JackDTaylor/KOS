﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace kOS
{
    public class BodyTarget : SpecialValue
    {
        public ExecutionContext context;
        public CelestialBody target;

        public BodyTarget(String name, ExecutionContext context) : this(VesselUtils.GetBodyByName(name), context) { }

        public BodyTarget(CelestialBody target, ExecutionContext context)
        {
            this.context = context;
            this.target = target;
        }

        public double GetDistance()
        {
            return Vector3d.Distance(context.Vessel.GetWorldPos3D(), target.position) - target.Radius;
        }

        public override object GetSuffix(string suffixName)
        {
            if (target == null) throw new kOSException("BODY structure appears to be empty!");

            if (suffixName == "NAME") return target.name;
            if (suffixName == "DESCRIPTION") return target.bodyDescription;
            if (suffixName == "MASS") return target.Mass;
            if (suffixName == "POSITION") return new Vector(target.position);
            if (suffixName == "ALTITUDE") return target.orbit.altitude;
            if (suffixName == "APOAPSIS") return target.orbit.ApA;
            if (suffixName == "PERIAPSIS") return target.orbit.PeA;
            if (suffixName == "RADIUS") return target.Radius;
            if (suffixName == "G") return target.GeeASL;
            if (suffixName == "MU") return target.gravParameter;
            if (suffixName == "ATM") return new BodyAtmosphere(target);
            if (suffixName == "VELOCITY") return new Vector(target.orbit.GetVel());
            if (suffixName == "DISTANCE") return (float)GetDistance();
            if (suffixName == "BODY") return new BodyTarget(target.orbit.referenceBody, context);

            return base.GetSuffix(suffixName);
        }

        public override string ToString()
        {
 	        if (target != null)
            {
                return "BODY(\"" + target.name + "\")";
            }

            return base.ToString();
        }
    }


}
