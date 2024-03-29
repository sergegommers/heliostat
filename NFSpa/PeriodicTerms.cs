﻿namespace NFSpa
{
    internal static class PeriodicTerms
    {
        internal const int L_COUNT = 6;
        internal const int B_COUNT = 2;
        internal const int R_COUNT = 5;
        internal const int Y_COUNT = 63;

        private static readonly float[][] lt1 =
        {
            new float[]            {175347046.0f,0f,0f},
            new float[]            {3341656.0f,4.6692568f,6283.07585f},
            new float[]            {34894.0f,4.6261f,12566.1517f},
            new float[]            {3497.0f,2.7441f,5753.3849f},
            new float[]            {3418.0f,2.8289f,3.5231f},
            new float[]            {3136.0f,3.6277f,77713.7715f},
            new float[]            {2676.0f,4.4181f,7860.4194f},
            new float[]            {2343.0f,6.1352f,3930.2097f},
            new float[]            {1324.0f,0.7425f,11506.7698f},
            new float[]            {1273.0f,2.0371f,529.691f},
            new float[]            {1199.0f,1.1096f,1577.3435f},
            new float[]            {990f,5.233f,5884.927f},
            new float[]            {902f,2.045f,26.298f},
            new float[]            {857f,3.508f,398.149f},
            new float[]            {780f,1.179f,5223.694f},
            new float[]            {753f,2.533f,5507.553f},
            new float[]            {505f,4.583f,18849.228f},
            new float[]            {492f,4.205f,775.523f},
            new float[]            {357f,2.92f,0.067f},
            new float[]            {317f,5.849f,11790.629f},
            new float[]            {284f,1.899f,796.298f},
            new float[]            {271f,0.315f,10977.079f},
            new float[]            {243f,0.345f,5486.778f},
            new float[]            {206f,4.806f,2544.314f},
            new float[]            {205f,1.869f,5573.143f},
            new float[]            {202f,2.458f,6069.777f},
            new float[]            {156f,0.833f,213.299f},
            new float[]            {132f,3.411f,2942.463f},
            new float[]            {126f,1.083f,20.775f},
            new float[]            {115f,0.645f,0.98f},
            new float[]            {103f,0.636f,4694.003f},
            new float[]            {102f,0.976f,15720.839f},
            new float[]            {102f,4.267f,7.114f},
            new float[]            {99f,6.21f,2146.17f},
            new float[]            {98f,0.68f,155.42f},
            new float[]            {86f,5.98f,161000.69f},
            new float[]            {85f,1.3f,6275.96f},
            new float[]            {85f,3.67f,71430.7f},
            new float[]            {80f,1.81f,17260.15f},
            new float[]            {79f,3.04f,12036.46f},
            new float[]            {75f,1.76f,5088.63f},
            new float[]            {74f,3.5f,3154.69f},
            new float[]            {74f,4.68f,801.82f},
            new float[]            {70f,0.83f,9437.76f},
            new float[]            {62f,3.98f,8827.39f},
            new float[]            {61f,1.82f,7084.9f},
            new float[]            {57f,2.78f,6286.6f},
            new float[]            {56f,4.39f,14143.5f},
            new float[]            {56f,3.47f,6279.55f},
            new float[]            {52f,0.19f,12139.55f},
            new float[]            {52f,1.33f,1748.02f},
            new float[]            {51f,0.28f,5856.48f},
            new float[]            {49f,0.49f,1194.45f},
            new float[]            {41f,5.37f,8429.24f},
            new float[]            {41f,2.4f,19651.05f},
            new float[]            {39f,6.17f,10447.39f},
            new float[]            {37f,6.04f,10213.29f},
            new float[]            {37f,2.57f,1059.38f},
            new float[]            {36f,1.71f,2352.87f},
            new float[]            {36f,1.78f,6812.77f},
            new float[]            {33f,0.59f,17789.85f},
            new float[]            {30f,0.44f,83996.85f},
            new float[]            {30f,2.74f,1349.87f},
            new float[]            {25f,3.16f,4690.48f}
        };

        private static readonly float[][] lt2 =
        {
            new float[]            {628331966747.0f,0f,0f},
            new float[]            {206059.0f,2.678235f,6283.07585f},
            new float[]            {4303.0f,2.6351f,12566.1517f},
            new float[]            {425.0f,1.59f,3.523f},
            new float[]            {119.0f,5.796f,26.298f},
            new float[]            {109.0f,2.966f,1577.344f},
            new float[]            {93f,2.59f,18849.23f},
            new float[]            {72f,1.14f,529.69f},
            new float[]            {68f,1.87f,398.15f},
            new float[]            {67f,4.41f,5507.55f},
            new float[]            {59f,2.89f,5223.69f},
            new float[]            {56f,2.17f,155.42f},
            new float[]            {45f,0.4f,796.3f},
            new float[]            {36f,0.47f,775.52f},
            new float[]            {29f,2.65f,7.11f},
            new float[]            {21f,5.34f,0.98f},
            new float[]            {19f,1.85f,5486.78f},
            new float[]            {19f,4.97f,213.3f},
            new float[]            {17f,2.99f,6275.96f},
            new float[]            {16f,0.03f,2544.31f},
            new float[]            {16f,1.43f,2146.17f},
            new float[]            {15f,1.21f,10977.08f},
            new float[]            {12f,2.83f,1748.02f},
            new float[]            {12f,3.26f,5088.63f},
            new float[]            {12f,5.27f,1194.45f},
            new float[]            {12f,2.08f,4694f},
            new float[]            {11f,0.77f,553.57f},
            new float[]            {10f,1.3f,6286.6f},
            new float[]            {10f,4.24f,1349.87f},
            new float[]            {9f,2.7f,242.73f},
            new float[]            {9f,5.64f,951.72f},
            new float[]            {8f,5.3f,2352.87f},
            new float[]            {6f,2.65f,9437.76f},
            new float[]            {6f,4.67f,4690.48f},
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
        };

        private static readonly float[][] lt3 =
        {
            new float[]            {52919.0f,0f,0f},
            new float[]            {8720.0f,1.0721f,6283.0758f},
            new float[]            {309.0f,0.867f,12566.152f},
            new float[]            {27f,0.05f,3.52f},
            new float[]            {16f,5.19f,26.3f},
            new float[]            {16f,3.68f,155.42f},
            new float[]            {10f,0.76f,18849.23f},
            new float[]            {9f,2.06f,77713.77f},
            new float[]            {7f,0.83f,775.52f},
            new float[]            {5f,4.66f,1577.34f},
            new float[]            {4f,1.03f,7.11f},
            new float[]            {4f,3.44f,5573.14f},
            new float[]            {3f,5.14f,796.3f},
            new float[]            {3f,6.05f,5507.55f},
            new float[]            {3f,1.19f,242.73f},
            new float[]            {3f,6.12f,529.69f},
            new float[]            {3f,0.31f,398.15f},
            new float[]            {3f,2.28f,553.57f},
            new float[]            {2f,4.38f,5223.69f},
            new float[]            {2f,3.75f,0.98f},
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
        };

        private static readonly float[][] lt4 =
        {
            new float[]            {289.0f,5.844f,6283.076f},
            new float[]            {35f,0f,0f},
            new float[]            {17f,5.49f,12566.15f},
            new float[]            {3f,5.2f,155.42f},
            new float[]            {1f,4.72f,3.52f},
            new float[]            {1f,5.3f,18849.23f},
            new float[]            {1f,5.97f,242.73f},
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 }
        };

        private static readonly float[][] lt5 =
        {
            new float[]            {114.0f,3.142f,0f},
            new float[]            {8f,4.13f,6283.08f},
            new float[]            {1f,3.84f,12566.15f},
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 }
        };

        private static readonly float[][] lt6 =
        {
            new float[]            {1f,3.14f,0f},
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
            new float[]            {0f,0f,0 },
        };

        // Earth Periodic Terms
        public static readonly float[][][] L_TERMS = new float[L_COUNT][][]
        {
            lt1,
            lt2,
            lt3,
            lt4,
            lt5,
            lt6,
        };

        private static readonly float[][] bt1 =
        {
            new float[]        {280.0f,3.199f,84334.662f},
            new float[]        {102.0f,5.422f,5507.553f},
            new float[]        {80f,3.88f,5223.69f},
            new float[]        {44f,3.7f,2352.87f},
            new float[]        {32f,4f,1577.34f}
        };

        private static readonly float[][] bt2 =
        {
            new float[]        {9f,3.9f,5507.55f},
            new float[]        {6f,1.73f,5223.69f},
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 }
        };

        public static readonly float[][][] B_TERMS = new float[B_COUNT][][]
        {
            bt1,
            bt2
        };

        private static readonly float[][] rt1 =
        {
            new float[]        {100013989.0f,0f,0f},
            new float[]        {1670700.0f,3.0984635f,6283.07585f},
            new float[]        {13956.0f,3.05525f,12566.1517f},
            new float[]        {3084.0f,5.1985f,77713.7715f},
            new float[]        {1628.0f,1.1739f,5753.3849f},
            new float[]        {1576.0f,2.8469f,7860.4194f},
            new float[]        {925.0f,5.453f,11506.77f},
            new float[]        {542.0f,4.564f,3930.21f},
            new float[]        {472.0f,3.661f,5884.927f},
            new float[]        {346.0f,0.964f,5507.553f},
            new float[]        {329.0f,5.9f,5223.694f},
            new float[]        {307.0f,0.299f,5573.143f},
            new float[]        {243.0f,4.273f,11790.629f},
            new float[]        {212.0f,5.847f,1577.344f},
            new float[]        {186.0f,5.022f,10977.079f},
            new float[]        {175.0f,3.012f,18849.228f},
            new float[]        {110.0f,5.055f,5486.778f},
            new float[]        {98f,0.89f,6069.78f},
            new float[]        {86f,5.69f,15720.84f},
            new float[]        {86f,1.27f,161000.69f},
            new float[]        {65f,0.27f,17260.15f},
            new float[]        {63f,0.92f,529.69f},
            new float[]        {57f,2.01f,83996.85f},
            new float[]        {56f,5.24f,71430.7f},
            new float[]        {49f,3.25f,2544.31f},
            new float[]        {47f,2.58f,775.52f},
            new float[]        {45f,5.54f,9437.76f},
            new float[]        {43f,6.01f,6275.96f},
            new float[]        {39f,5.36f,4694f},
            new float[]        {38f,2.39f,8827.39f},
            new float[]        {37f,0.83f,19651.05f},
            new float[]        {37f,4.9f,12139.55f},
            new float[]        {36f,1.67f,12036.46f},
            new float[]        {35f,1.84f,2942.46f},
            new float[]        {33f,0.24f,7084.9f},
            new float[]        {32f,0.18f,5088.63f},
            new float[]        {32f,1.78f,398.15f},
            new float[]        {28f,1.21f,6286.6f},
            new float[]        {28f,1.9f,6279.55f},
            new float[]        {26f,4.59f,10447.39f}
         };

        private static readonly float[][] rt2 =
        {
            new float[]        {103019.0f,1.10749f,6283.07585f},
            new float[]        {1721.0f,1.0644f,12566.1517f},
            new float[]        {702.0f,3.142f,0f},
            new float[]        {32f,1.02f,18849.23f},
            new float[]        {31f,2.84f,5507.55f},
            new float[]        {25f,1.32f,5223.69f},
            new float[]        {18f,1.42f,1577.34f},
            new float[]        {10f,5.91f,10977.08f},
            new float[]        {9f,1.42f,6275.96f},
            new float[]        {9f,0.27f,5486.78f},
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
        };

        private static readonly float[][] rt3 =
        {
            new float[]        {4359.0f,5.7846f,6283.0758f},
            new float[]        {124.0f,5.579f,12566.152f},
            new float[]        {12f,3.14f,0f},
            new float[]        {9f,3.63f,77713.77f},
            new float[]        {6f,1.87f,5573.14f},
            new float[]        {3f,5.47f,18849.23f},
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
        };

        private static readonly float[][] rt4 =
        {
            new float[]        {145.0f,4.273f,6283.076f},
            new float[]        {7f,3.92f,12566.15f},
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
        };

        private static readonly float[][] rt5 =
        {
            new float[]        {4f,2.56f,6283.08f},
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            new float[]        {0f,0f,0 },
            };

        public static readonly float[][][] R_TERMS = new float[R_COUNT][][]
        {
            rt1,
            rt2,
            rt3,
            rt4,
            rt5
        };

        ////////////////////////////////////////////////////////////////
        ///  Periodic Terms for the nutation in longitude and obliquity
        ////////////////////////////////////////////////////////////////
        public static readonly int[][] Y_TERMS = new int[Y_COUNT][]
        {
            new int[] {0,0,0,0,1},
            new int[] {-2,0,0,2,2},
            new int[] {0,0,0,2,2},
            new int[] {0,0,0,0,2},
            new int[] {0,1,0,0,0},
            new int[] {0,0,1,0,0},
            new int[] {-2,1,0,2,2},
            new int[] {0,0,0,2,1},
            new int[] {0,0,1,2,2},
            new int[] {-2,-1,0,2,2},
            new int[] {-2,0,1,0,0},
            new int[] {-2,0,0,2,1},
            new int[] {0,0,-1,2,2},
            new int[] {2,0,0,0,0},
            new int[] {0,0,1,0,1},
            new int[] {2,0,-1,2,2},
            new int[] {0,0,-1,0,1},
            new int[] {0,0,1,2,1},
            new int[] {-2,0,2,0,0},
            new int[] {0,0,-2,2,1},
            new int[] {2,0,0,2,2},
            new int[] {0,0,2,2,2},
            new int[] {0,0,2,0,0},
            new int[] {-2,0,1,2,2},
            new int[] {0,0,0,2,0},
            new int[] {-2,0,0,2,0},
            new int[] {0,0,-1,2,1},
            new int[] {0,2,0,0,0},
            new int[] {2,0,-1,0,1},
            new int[] {-2,2,0,2,2},
            new int[] {0,1,0,0,1},
            new int[] {-2,0,1,0,1},
            new int[] {0,-1,0,0,1},
            new int[] {0,0,2,-2,0},
            new int[] {2,0,-1,2,1},
            new int[] {2,0,1,2,2},
            new int[] {0,1,0,2,2},
            new int[] {-2,1,1,0,0},
            new int[] {0,-1,0,2,2},
            new int[] {2,0,0,2,1},
            new int[] {2,0,1,0,0},
            new int[] {-2,0,2,2,2},
            new int[] {-2,0,1,2,1},
            new int[] {2,0,-2,0,1},
            new int[] {2,0,0,0,1},
            new int[] {0,-1,1,0,0},
            new int[] {-2,-1,0,2,1},
            new int[] {-2,0,0,0,1},
            new int[] {0,0,2,2,1},
            new int[] {-2,0,2,0,1},
            new int[] {-2,1,0,2,1},
            new int[] {0,0,1,-2,0},
            new int[] {-1,0,1,0,0},
            new int[] {-2,1,0,0,0},
            new int[] {1,0,0,0,0},
            new int[] {0,0,1,2,0},
            new int[] {0,0,-2,2,2},
            new int[] {-1,-1,1,0,0},
            new int[] {0,1,1,0,0},
            new int[] {0,-1,1,2,2},
            new int[] {2,-1,-1,2,2},
            new int[] {0,0,3,2,2},
            new int[] {2,-1,0,2,2},
        };

        public static readonly float[][] PE_TERMS = new float[Y_COUNT][]
        {
            new float[] {-171996f,-174.2f,92025f,8.9f},
            new float[] {-13187f,-1.6f,5736f,-3.1f},
            new float[] {-2274f,-0.2f,977f,-0.5f},
            new float[] {2062f,0.2f,-895f,0.5f},
            new float[] {1426f,-3.4f,54f,-0.1f},
            new float[] {712f,0.1f,-7f,0f},
            new float[] {-517f,1.2f,224f,-0.6f},
            new float[] {-386f,-0.4f,200f,0f},
            new float[] {-301f,0f,129f,-0.1f},
            new float[] {217f,-0.5f,-95f,0.3f},
            new float[] {-158f,0f,0f,0f},
            new float[] {129f,0.1f,-70f,0f},
            new float[] {123f,0f,-53f,0f},
            new float[] {63f,0f,0f,0f},
            new float[] {63f,0.1f,-33f,0f},
            new float[] {-59f,0f,26f,0f},
            new float[] {-58f,-0.1f,32f,0f},
            new float[] {-51f,0f,27f,0f},
            new float[] {48f,0f,0f,0f},
            new float[] {46f,0f,-24f,0f},
            new float[] {-38f,0f,16f,0f},
            new float[] {-31f,0f,13f,0f},
            new float[] {29f,0f,0f,0f},
            new float[] {29f,0f,-12f,0f},
            new float[] {26f,0f,0f,0f},
            new float[] {-22f,0f,0f,0f},
            new float[] {21f,0f,-10f,0f},
            new float[] {17f,-0.1f,0f,0f},
            new float[] {16f,0f,-8f,0f},
            new float[] {-16f,0.1f,7f,0f},
            new float[] {-15f,0f,9f,0f},
            new float[] {-13f,0f,7f,0f},
            new float[] {-12f,0f,6f,0f},
            new float[] {11f,0f,0f,0f},
            new float[] {-10f,0f,5f,0f},
            new float[] {-8f,0f,3f,0f},
            new float[] {7f,0f,-3f,0f},
            new float[] {-7f,0f,0f,0f},
            new float[] {-7f,0f,3f,0f},
            new float[] {-7f,0f,3f,0f},
            new float[] {6f,0f,0f,0f},
            new float[] {6f,0f,-3f,0f},
            new float[] {6f,0f,-3f,0f},
            new float[] {-6f,0f,3f,0f},
            new float[] {-6f,0f,3f,0f},
            new float[] {5f,0f,0f,0f},
            new float[] {-5f,0f,3f,0f},
            new float[] {-5f,0f,3f,0f},
            new float[] {-5f,0f,3f,0f},
            new float[] {4f,0f,0f,0f},
            new float[] {4f,0f,0f,0f},
            new float[] {4f,0f,0f,0f},
            new float[] {-4f,0f,0f,0f},
            new float[] {-4f,0f,0f,0f},
            new float[] {-4f,0f,0f,0f},
            new float[] {3f,0f,0f,0f},
            new float[] {-3f,0f,0f,0f},
            new float[] {-3f,0f,0f,0f},
            new float[] {-3f,0f,0f,0f},
            new float[] {-3f,0f,0f,0f},
            new float[] {-3f,0f,0f,0f},
            new float[] {-3f,0f,0f,0f},
            new float[] {-3f,0f,0f,0f},
        };
    }
}
