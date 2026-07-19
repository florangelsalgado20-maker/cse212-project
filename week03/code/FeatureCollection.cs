using System.Collections.Generic;

public class FeatureCollection
{
    public List<Feature> features { get; set; }
}

public class Feature
{
    public FeatureProperties properties { get; set; }
}

public class FeatureProperties
{
    public string place { get; set; }
    public double mag { get; set; }
}
