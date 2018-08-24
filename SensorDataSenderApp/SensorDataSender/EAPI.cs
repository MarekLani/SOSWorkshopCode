using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileReaderApp
{
    public class EAPI
    {
        /*---------------------------------------
        *
        *
        * S T A T U S C O D E S
        *
        *
        ---------------------------------------*/
        /* Description
        * The EAPI library is not yet or unsuccessfully initialized.
        * EApiLibInitialize needs to be called prior to the first access of any
        * other EAPI function.
        * Actions
        * Call EApiLibInitialize..
        */
        public const UInt32 EAPI_STATUS_NOT_INITIALIZED = 0xFFFFFFFF;

        /* Description
        * Library is initialized.
        * Actions
        * none.
        */
        public const UInt32 EAPI_STATUS_INITIALIZED = 0xFFFFFFFE;

        /* Description
        * Memory Allocation Error.
        * Actions
        * Free memory and try again..
        */
        public const UInt32 EAPI_STATUS_ALLOC_ERROR = 0xFFFFFFFD;

        /* Description
        * Time out in driver. This is Normally caused by hardware/software
        * semaphore timeout.
        * Actions
        * Retry.
        */
        public const UInt32 EAPI_STATUS_DRIVER_TIMEOUT = 0xFFFFFFFC;

        /* Description
        * Hardware maybe not ready.
        * Actions
        * Try to verify BIOS setting.
        */
        public const UInt32 EAPI_STATUS_DEVICE_NOT_READY = 0xFFFFFFFB;

        /* Description
        * One or more of the EAPI function call parameters are out of the
        * defined range.
        *
        * Possible Reasons include be
        * NULL Pointer
        * Invalid Offset
        * Invalid Length
        * Undefined Value
        *
        * Storage Write
        * Incorrectly Aligned Offset
        * Invalid Write Length
        * Actions
        * Verify Function Parameters.
        */
        public const UInt32 EAPI_STATUS_INVALID_PARAMETER = 0xFFFFFEFF;

        /* Description
        * The Block Alignment is incorrect.
        * Actions
        * Use pInputs and pOutputs to correctly select input and outputs.
        */
        public const UInt32 EAPI_STATUS_INVALID_BLOCK_ALIGNMENT = 0xFFFFFEFE;

        /* Description
        * This means that the Block length is too long.
        * Actions
        * Use Alignment Capabilities information to correctly align write access.
        */
        public const UInt32 EAPI_STATUS_INVALID_BLOCK_LENGTH = 0xFFFFFEFD;

        /* Description
        * The current Direction Argument attempts to set GPIOs to a unsupported
        * directions. I.E. Setting GPI to Output.
        * Actions
        * Use pInputs and pOutputs to correctly select input and outputs.
        */
        public const UInt32 EAPI_STATUS_INVALID_DIRECTION = 0xFFFFFEFC;

        /* Description
        * The Bitmask Selects bits/GPIOs which are not supported for the current ID.
        * Actions
        * Use pInputs and pOutputs to probe supported bits..
        */
        public const UInt32 EAPI_STATUS_INVALID_BITMASK = 0xFFFFFEFB;

        /* Description
        * Watchdog timer already started.
        * Actions
        * Call EApiWDogStop, before retrying.
        */
        public const UInt32 EAPI_STATUS_RUNNING = 0xFFFFFEFA;

        /* Description
        * This function or ID is not supported at the actual hardware environment.
        * Actions
        * none.
        */
        public const UInt32 EAPI_STATUS_UNSUPPORTED = 0xFFFFFCFF;

        /* Description
        * I2C Device Error
        * No Acknowledge For Device Address, 7Bit Address Only
        * 10Bit Address may cause Write error if 2 10Bit addressed devices
        * present on the bus.
        * Actions
        * none.
        */
        public const UInt32 EAPI_STATUS_NOT_FOUND = 0xFFFFFBFF;

        /* Description
        * I2C Time-out
        * Device Clock stretching time-out, Clock pulled low by device
        * for too long
        * Actions
        * none.
        */
        public const UInt32 EAPI_STATUS_TIMEOUT = 0xFFFFFBFE;

        /* Description
        * EApi I2C functions specific. The addressed I2C bus is busy or there
        * is a bus collision.
        * The I2C bus is in use. Either CLK or DAT are low.
        * Arbitration loss or bus Collision, data remains low when writing a 1
        * Actions
        * Retry.
        */
        public const UInt32 EAPI_STATUS_BUSY_COLLISION = 0xFFFFFBFD;

        /* Description
        * I2C Read Error
        * Not Possible to detect.
        * Storage Read Error
        * ....
        * Actions
        * Retry.
        */
        public const UInt32 EAPI_STATUS_READ_ERROR = 0xFFFFFAFF;

        /* Description
        * I2C Write Error
        * No Acknowledge received after writing any Byte after the First Address
        * Byte.
        * Can be caused by
        * unsupported Device Command/Index
        * Ext Command/Index used on Standard Command/Index Device
        * 10Bit Address Device Not Present
        * Storage Write Error
        * ...
        * Actions
        * Retry.
        */
        public const UInt32 EAPI_STATUS_WRITE_ERROR = 0xFFFFFAFE;

        /* Description
        * The amount of available data exceeds the buffer size.
        * Storage buffer overflow was prevented. Read count was larger then
        * the defined buffer length.
        * Read Count > Buffer Length
        * Actions
        * Either increase the buffer size or reduce the block length.
        */
        public const UInt32 EAPI_STATUS_MORE_DATA = 0xFFFFF9FF;

        /* Description
        * Generic error message. No further error details are available.
        * Actions
        * none.
        */
        public const UInt32 EAPI_STATUS_ERROR = 0xFFFFF0FF;

        /* Description
        * The operation was successful.
        * Actions
        * none.
        */
        public const UInt32 EAPI_STATUS_SUCCESS = 0x00000000;


        /*---------------------------------------
        *
        *
        * IDs
        *
        *
        ---------------------------------------*/
        /*
        *
        * B O A R D I N F O M A T I O N S T R I N G S
        *
        */
        public const UInt32 EAPI_ID_BOARD_MANUFACTURER_STR = 0;     /* Board Manufacturer
                                                                    * Name String
														            */
        public const UInt32 EAPI_ID_BOARD_NAME_STR = 1;             /* Board Name String */
        public const UInt32 EAPI_ID_BOARD_REVISION_STR = 2;         /* Board Name String */
        public const UInt32 EAPI_ID_BOARD_SERIAL_STR = 3;           /* Board Serial Number String */
        public const UInt32 EAPI_ID_BOARD_BIOS_REVISION_STR = 4;    /* Board Bios Revision
														            * String
                                                                    */
        public const UInt32 EAPI_ID_BOARD_HW_REVISION_STR = 5;      /* Board Hardware
														            * Revision String
														            */
        public const UInt32 EAPI_ID_BOARD_PLATFORM_TYPE_STR = 6;    /* Platform ID
														            * (ETX, COM Express,
														            * etc...)
														            */
        public const UInt32 EAPI_ID_EC_REVISION_STR = 7;            /* EC Revision
                                                                    * 
                                                                    * 
                                                                    */

        /*
        *
        * B O A R D I N F O M A T I O N V A L U E S
        *
        */
        public const UInt32 EAPI_ID_GET_EAPI_SPEC_VERSION = 0;          /* EAPI Specification
																	    * Revision I.E.The
																	    * EAPI Spec Version
                                                                        * Bits 31-24, Revision
                                                                        * 23-16, 15-0 always 0
																	    * Used to implement
                                                                        * this interface
																	    */
        public const UInt32 EAPI_ID_BOARD_BOOT_COUNTER_VAL = 1;         /* Units = Boots */
        public const UInt32 EAPI_ID_BOARD_RUNNING_TIME_METER_VAL = 2;   /* Units = Minutes */
        public const UInt32 EAPI_ID_BOARD_PNPID_VAL = 3;                /* Encoded PNP ID
																	    * Format
                                                                        * (Compressed ASCII)
																	    */
        public const UInt32 EAPI_ID_BOARD_PLATFORM_REV_VAL = 4;         /* Platform Revision
                                                                        * I.E.The PICMG Spec
                                                                        * Version Bits 31-24,
                                                                        * Revision 23-16,
                                                                        * 15-0 always 0
                                                                        */
        public const UInt32 EAPI_ID_AONCUS_HISAFE_FUCTION = 5;             /* Check function need
																	    * to turn on or off
                                                                        */

        public const UInt32 EAPI_ID_BOARD_DRIVER_VERSION_VAL = 0x10000; /* Vendor Specific
																	    * (Optional)
																	    */
        public const UInt32 EAPI_ID_BOARD_LIB_VERSION_VAL = 0x10001;	/* Vendor Specific
																	    * (Optional)
																	    */

        public const UInt32 EAPI_ID_HWMON_CPU_TEMP = 0x20000;           /* 0.1 Kelvins */
        public const UInt32 EAPI_ID_HWMON_CHIPSET_TEMP = 0x20001;       /* 0.1 Kelvins */
        public const UInt32 EAPI_ID_HWMON_SYSTEM_TEMP = 0x20002;        /* 0.1 Kelvins */

        public const UInt32 EAPI_KELVINS_OFFSET = 2731;
        public static UInt32 EAPI_ENCODE_CELCIUS(UInt32 Celsius)
        {
            return (Celsius * 10) + EAPI_KELVINS_OFFSET;
        }
        public static UInt32 EAPI_DECODE_CELCIUS(UInt32 Celsius)
        {
            return ((Celsius) - EAPI_KELVINS_OFFSET) / 10;
        }
        public const UInt32 EAPI_ID_HWMON_VOLTAGE_VCORE = 0x21004;      /* millivolts */
        public const UInt32 EAPI_ID_HWMON_VOLTAGE_2V5 = 0x21008;        /* millivolts */
        public const UInt32 EAPI_ID_HWMON_VOLTAGE_3V3 = 0x2100C;        /* millivolts */
        public const UInt32 EAPI_ID_HWMON_VOLTAGE_VBAT = 0x21010;       /* millivolts */
        public const UInt32 EAPI_ID_HWMON_VOLTAGE_5V = 0x21014;         /* millivolts */
        public const UInt32 EAPI_ID_HWMON_VOLTAGE_5VSB = 0x21018;       /* millivolts */
        public const UInt32 EAPI_ID_HWMON_VOLTAGE_12V = 0x2101C;        /* millivolts */
        public const UInt32 EAPI_ID_HWMON_VOLTAGE_DIMM = 0x21020;       /* millivolts */
        public const UInt32 EAPI_ID_HWMON_VOLTAGE_3VSB = 0x21024;       /* millivolts */   //modify by Tim 0429
        public const UInt32 EAPI_ID_HWMON_FAN_CPU = 0x22000;            /* RPM */
        public const UInt32 EAPI_ID_HWMON_FAN_CHIPSET = 0x22001;        /* RPM */
        public const UInt32 EAPI_ID_HWMON_FAN_SYSTEM = 0x22002;         /* RPM */

        /*
        *
        * B A C K L I G H T
        *
        */
        public const UInt32 EAPI_ID_BACKLIGHT_1 = 0;
        public const UInt32 EAPI_ID_BACKLIGHT_2 = 1;
        public const UInt32 EAPI_ID_BACKLIGHT_3 = 2;
        /* Backlight Values */
        public const UInt32 EAPI_BACKLIGHT_SET_ON = 0;
        public const UInt32 EAPI_BACKLIGHT_SET_OFF = 0xFFFFFFFF;
        public const UInt32 EAPI_BACKLIGHT_SET_DIMEST = 0;
        public const UInt32 EAPI_BACKLIGHT_SET_BRIGHTEST = 255;
        /*
        *
        * S T O R A G E
        *
        */
        public const UInt32 EAPI_ID_STORAGE_STD = 0;
        /*
        *
        * I 2 C
        *
        */
        public const UInt32 EAPI_ID_I2C_EXTERNAL = 0;   /* Baseboard I2C Interface required */
        public const UInt32 EAPI_ID_I2C_LVDS_1 = 1;     /* LVDS/EDP 1 Interface (optional) */
        public const UInt32 EAPI_ID_I2C_LVDS_2 = 2;     /* LVDS/EDP 2 Interface (optional) */
        public const UInt32 EAPI_ID_AONCUS_I2C_EXTERNAL_2 = 3;   /* Baseboard 2nd I2C Interface */
        public const UInt32 EAPI_ID_AONCUS_I2C_EXTERNAL_3 = 4;   /* Baseboard 3rd I2C Interface */
        public const UInt32 EAPI_ID_AONCUS_SMBUS_EXTERNAL_1 = 5;     /* Baseboard first SMBUS Interface */
        public const UInt32 EAPI_ID_AONCUS_SMBUS_EXTERNAL_2 = 6;     /* Baseboard 2nd SMBUS Interface */
        public const UInt32 EAPI_ID_AONCUS_SMBUS_EXTERNAL_3 = 7;	 /* Baseboard 3rd SMBUS Interface */
        //public const UInt32 EAPI_ID_SMBUS_EXTERNAL = 3;

        public UInt32 EAPI_I2C_ENC_7BIT_ADDR(UInt32 x)
        {
            return ((x) & 0x07F) << 1;
        }
        public UInt32 EAPI_I2C_DEC_7BIT_ADDR(UInt32 x)
        {
            return ((x) >> 1) & 0x07F;
        }

        public UInt32 EAPI_I2C_ENC_STD_CMD(UInt32 x)
        {
            return ((x) & 0xFF) | (0 << 30);
        }
        public UInt32 EAPI_I2C_ENC_EXT_CMD(UInt32 x)
        {
            return ((x) & 0xFFFF) | ((UInt32)2 << 30);
        }

        public bool EAPI_I2C_IS_EXT_CMD(UInt32 x)
        {
            return ((x) & ((UInt32)3 << 30)) == ((UInt32)2 << 30);
        }
        public bool EAPI_I2C_IS_STD_CMD(UInt32 x)
        {
            return ((x) & ((UInt32)3 << 30)) == (0 << 30);
        }
        public bool EAPI_I2C_IS_NO_CMD(UInt32 x)
        {
            return ((x) & ((UInt32)3 << 30)) == ((UInt32)1 << 30);
        }

        /*
        *
        * G P I O
        *
        */
        /* IDs */
        /*
        * Individual ID Per GPIO Mapping
        */
        public static UInt32 EAPI_GPIO_GPIO_ID(UInt32 GPIO_NUM)
        {
            return GPIO_NUM;
        }
        public const UInt32 EAPI_GPIO_GPIO_BITMASK = 1;
        public UInt32 EAPI_ID_GPIO_GPIO00 = EAPI_GPIO_GPIO_ID(0); /* (Optional) */
        public UInt32 EAPI_ID_GPIO_GPIO01 = EAPI_GPIO_GPIO_ID(1); /* (Optional) */
        public UInt32 EAPI_ID_GPIO_GPIO02 = EAPI_GPIO_GPIO_ID(2); /* (Optional) */
        public UInt32 EAPI_ID_GPIO_GPIO03 = EAPI_GPIO_GPIO_ID(3); /* (Optional) */
        public UInt32 EAPI_ID_GPIO_GPIO04 = EAPI_GPIO_GPIO_ID(4); /* (Optional) */
        public UInt32 EAPI_ID_GPIO_GPIO05 = EAPI_GPIO_GPIO_ID(5); /* (Optional) */
        public UInt32 EAPI_ID_GPIO_GPIO06 = EAPI_GPIO_GPIO_ID(6); /* (Optional) */
        public UInt32 EAPI_ID_GPIO_GPIO07 = EAPI_GPIO_GPIO_ID(7); /* (Optional) */

        /*
        * Multiple GPIOs Per ID Mapping
        */
        public static UInt32 EAPI_GPIO_BANK_ID(UInt32 GPIO_NUM)
        {
            return (0x10000 | ((GPIO_NUM) >> 5));
        }
        public static int EAPI_GPIO_BANK_MASK(int GPIO_NUM)
        {
            return (0x01 << (GPIO_NUM & 0x1F));
        }
        public UInt32 EAPI_ID_GPIO_BANK00 = EAPI_GPIO_BANK_ID(0); /* GPIOs 0 - 31 (optional) */
        public UInt32 EAPI_ID_GPIO_BANK01 = EAPI_GPIO_BANK_ID(32); /* GPIOs 32 - 63 (optional) */
        public UInt32 EAPI_ID_GPIO_BANK02 = EAPI_GPIO_BANK_ID(64); /* GPIOs 64 - 95 (optional) */

        /* Bit mask Bit States */
        public const UInt32 EAPI_GPIO_BITMASK_SELECT = 1;
        public const UInt32 EAPI_GPIO_BITMASK_NOSELECT = 0;
        /* Levels */
        public const UInt32 EAPI_GPIO_LOW = 0;
        public const UInt32 EAPI_GPIO_HIGH = 1;
        /* Directions */
        public const UInt32 EAPI_GPIO_INPUT = 1;
        public const UInt32 EAPI_GPIO_OUTPUT = 0;
    }
}
