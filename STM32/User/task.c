	
#include "task.h"
#include "bsp_led.h"
#include "bsp_usart.h"
#include "string.h"
#include "math.h"



/*
  Time manage for delay
*/
#define    TIME_DELAY_MS    100000

volatile uint32_t sysTimeTick;





void TASK_LedCtrl( void );
void TASK_Usart( void );


void TASK_Loop( void )
{	
	  if (sysTimeTick >= TIME_DELAY_MS)
		{
			  sysTimeTick = 0;
			
			  TASK_Usart();
		}
}




/*
  LED
*/
void TASK_LedCtrl( void )
{
	  LED1_TOGGLE;
}




/*
  USART
*/
#define    DATA_LENGTH     (6)
#define    STEAM_LENGTH    (1 + DATA_LENGTH * sizeof(int16_t))

#define    STREAM_HEAD     ('y')
#define    PI_6            ((double)0.524)

__packed struct Stream 
{
	uint8_t head;
	int16_t data[DATA_LENGTH]; 
} stream;


void TASK_Usart( void )
{
    uint8_t temp[STEAM_LENGTH] = {0};
		int16_t i = 0;
		
		stream.head    = STREAM_HEAD;
    stream.data[0] = (int16_t)(sin(0)      * 1000);
    stream.data[1] = (int16_t)(sin(PI_6)   * 1000);
    stream.data[2] = (int16_t)(sin(PI_6*2) * 1000);
    stream.data[3] = (int16_t)(sin(PI_6*3) * 1000);
    stream.data[4] = (int16_t)(sin(PI_6*4) * 1000);
    stream.data[5] = (int16_t)(sin(PI_6*5) * 1000);
	  
		memcpy(temp, &stream, STEAM_LENGTH);
		
		for (i = 0; i < STEAM_LENGTH; i++)
		{
		    Usart_SendByte( DEBUG_USARTx, temp[i] );	
		}
	  
}
