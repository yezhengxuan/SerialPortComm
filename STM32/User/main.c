	
#include "bsp_init.h"
#include "task.h"



int main(void)
{	
    BSP_Init( );
	
		while (1)
		{
        TASK_Loop( );
		}
}
