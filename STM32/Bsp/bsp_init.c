
#include "bsp_init.h"
#include "bsp_led.h"
#include "bsp_SysTick.h"
#include "bsp_usart.h"


void BSP_Init( void )
{
	  LED_GPIO_Config( );
	  USART_Config( );
    SysTick_Init( );
}
