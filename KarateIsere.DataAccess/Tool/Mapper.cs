using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace KarateIsere.DataAccess.Tool {
    public static class Mapper {
        private static Logger log = LogManager.GetCurrentClassLogger();

        public static void Map ( object srcObj, object dstObj ) {
            Contract.Requires( srcObj != null );
            Contract.Requires( dstObj != null );
            log.Trace( "Map parameters" );
            log.Trace( "Destination object :" +dstObj );
            log.Trace( "Source object : " + srcObj );

            Type srcType = srcObj.GetType();
            Type dstType = dstObj.GetType();

            Type type = FirstCommonType( srcType, dstType );
            Map( srcObj, dstObj, type );
        }

        private static void Map ( object srcObj, object dstObj, Type type ) {
            Contract.Requires( type!=null );
            Contract.Requires( srcObj!=null );
            Contract.Requires( dstObj!=null );

            log.Trace( "Map parameters" );
            log.Trace( "Destination object :" +dstObj );
            log.Trace( "Source object : " + srcObj );
            log.Trace( "Common type :" + type );

            PropertyInfo[] infos = type.GetProperties();
            foreach (PropertyInfo pi in infos) {
                //Get the corresponding value in the srcObj
                object value = type.GetProperty( pi.Name ).GetValue( srcObj );
                pi.SetValue( dstObj, value );
            }
        }

        private static Type FirstCommonType ( Type srcType, Type dstType ) {
            log.Trace( "FirstCommonType parameters" );
            log.Trace( "Source type :" + srcType.ToString() );
            log.Trace( "Destination type :" + dstType.ToString() );

            while (srcType != typeof( object )) {
                Type initDstType = dstType;

                while (dstType != typeof( object )) {
                    if (srcType == dstType) {
                        return srcType;
                    }

                    dstType = dstType.BaseType;
                }

                dstType = initDstType;
                srcType = srcType.BaseType;
            }

            return null;
        }
    }
}
