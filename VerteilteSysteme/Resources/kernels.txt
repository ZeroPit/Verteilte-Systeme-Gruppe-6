__kernel void helloWorld() 
{
	printf("%s\n", "Hello OpenCL"); 
}

__kernel void sumTwoIntegers(__global int * pInt1,__global int * pInt2)
{
	int i = get_global_id(0);	
	printf("Sum %d: %d + %d",i + 1, pInt1[i] ,pInt2[i]);
	pInt1[i] = pInt1[i] + pInt2[i];
	printf(" = %d\n", pInt1[i]);
}

#ifdef cl_khr_fp64
    #pragma OPENCL EXTENSION cl_khr_fp64 : enable
#elif defined(cl_amd_fp64)
    #pragma OPENCL EXTENSION cl_amd_fp64 : enable
#else
    #error "Double precision floating point not supported by OpenCL implementation."
#endif

__kernel void calculateMandel(__global int * pCalculation,__global double * pStartValues)
{	
	int i = get_global_id(0);	  
   
    double xmin = pStartValues[0];
    double ymin = pStartValues[1];
    double xmax = pStartValues[2];
    double ymax = pStartValues[3];
	int lW = pStartValues[4];
	int lH = pStartValues[5];

    double intigralX = (xmax - xmin) / lW;
    double intigralY = (ymax - ymin) / lH;   

	int s =  i % lW;
	int z  = i / lW ;

	double x = xmin + s * intigralX;
	double y = ymin + z * intigralY;

	double x1 = 0;
    double y1 = 0;

	int looper = 0; 
	double  xx = 0.0;
	while(looper <= 254 && sqrt((x1 * x1) + (y1 * y1)) < 2)
	{
		looper = looper + 1 ;
		xx = (x1 * x1) - (y1 * y1) + x;
		y1 = 2 * x1 * y1 + y;
		x1 = xx;
	}
	double perc = looper / (255.0);
    int val = ((int)(perc * 255));
	pCalculation[i]= val;
}