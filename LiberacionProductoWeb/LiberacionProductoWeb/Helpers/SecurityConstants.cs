using System;
namespace LiberacionProductoWeb.Helpers
{
    public class SecurityConstants
    {
        //Perfiles
        public const string PERFIL_ADMIN = "Administrador";
        public const string PERFIL_RESPONSABLE_SANITARIO = "Responsable Sanitario";
        public const string PERFIL_DESIGNADO_DEL_RESPONSABLE_SANITARIO = "Designado del Responsable Sanitario";
        public const string PERFIL_LLENADOR_DE_PIPAS = "Llenador de Pipas";
        public const string PERFIL_SUPERINTENDENTE_DE_PLANTA = "Superintendente de Planta";
        public const string PERFIL_GERENTE_DE_PRODUCCION = "Gerente de Produccion";
        public const string PERFIL_USUARIO_DE_PRODUCCION = "Usuario de Produccion";
        public const string PERMISSION = "Permission";

        //Permissions


        public const string CONSULTAR_MIS_TAREAS_PENDIENTES = "e1";
        public const string CONSULTAR_EXPEDIENTE_DE_LOTE = "e2";
        public const string CONSULTA_GENERAL = "e3";
        public const string CREAR_OP = "e4";
        public const string EDITAR_OP = "e5";
        public const string CANCELAR_OP = "e6";
        public const string APROBAR_CANCELACION_OP = "e8";
        public const string CONSULTAR_OP = "e9";
        public const string EXPORTAR_OP = "e10";
        public const string EDITAR_OA = "e11";
        public const string CONSULTAR_OA = "e12";
        public const string EXPORTAR_OA = "e13";
        public const string EDITAR_VERIFICACION_DE_PIPAS = "e14";
        public const string CONSULTAR_VERIFICACION_DE_PIPAS = "e15";
        public const string EXPORTAR_CHECK_LIST_VERIFICACION_DE_PIPAS = "e16";
        public const string CANCELAR_CHECK_LIST_DE_VERIFICACION_DE_PIPAS = "e17";
        public const string CONSULTAR_RAP_TANQUES =	"e18";
        public const string CONSULTAR_RAP_PIPAS =	"e19";
        public const string COMPLEMENTO_DE_RAP_PIPAS =	"e20";
        public const string CONSULTAR_REPORTE_AUDIT_TRAIL = "e22";
        public const string COMPLEMENTO_DE_RAP_TANQUES =	"e24";
        public const string CONSULTAR_CATALOGO_DE_USUARIOS = "c1";
        public const string EDITAR_CATALOGO_DE_USUARIOS = "c2";
        public const string ELIMINAR_REGISTRO_EN_CATALOGO_DE_USUARIOS = "c3";
        public const string CONSULTAR_CATALOGO_DE_ROLES = "c4";
        public const string EDITAR_CATALOGO_DE_ROLES = "c5";
        public const string ELIMINAR_ROL_DEL_CATALOGO_DE_ROLES = "c6";
        public const string CONSULTAR_CATALOGO_GENERAL = "c7";
        public const string EDITAR_CATALOGO_GENERAL = "c8";
        public const string ELIMINAR_REGISTRO_DE_CATALOGO_GENERAL = "c9";
        public const string CONSULTAR_CATALOGO_FORMULA_FARMACEUTICA = "c10";
        public const string EDITAR_CATALOGO_FORMULA_FARMACEUTICA = "c11";
        public const string ELIMINAR_REGISTRO_DE_CATALOGO_FORMULA_FARMACEUTICA = "c12";
        public const string CONSULTAR_CATALOGO_PRODUCTO = "c13";
        public const string EDITAR_CATALOGO_DE_PRODUCTO = "c14";
        public const string ELIMINAR_REGISTRO_DE_CATALOGO_PRODUCTO = "c15";
        public const string CONSULTAR_CATALOGO_ESTUDIO_DE_ESTABILIDAD_DEL_PRODUCTO = "c16";
        public const string EDITAR_CATALOGO_ESTUDIO_DE_ESTABILIDAD_DEL_PRODUCTO = "c17";
        public const string ELIMINAR_REGISTRO_DE_CATALOGO_ESTUDIO_DE_ESTABILIDAD_DEL_PRODUCTO = "c18";
        public const string CONSULTAR_CATALOGO_DISPOSICION_DE_PNC = "c19";
        public const string EDITAR_CATALOGO_DISPOSICION_DE_PNC = "c20";
        public const string ELIMINAR_REGISTRO_DE_CATALOGO_DISPOSICION_DE_PNC = "c21";
        public const string CONSULTAR_ENVASE_PRIMARIO = "c22";
        public const string EDITAR_ENVASE_PRIMARIO = "c23";
        public const string ELIMINAR_ENVASE_PRIMARIO = "c24";
        public const string CONSULTAR_LAYOUT_CERTIFICADO = "c25";
        public const string EDITAR_LAYOUT_CERTIFICADO = "c26";



        //default User
        public const string ADMIN_USER = "admin";
        public const string ADMIN_EMAIL = "admin@linde.com";
        public const string ADMIN_PASS = "a1b2c3d4.";
        public const string ADMIN_NOMBRE = "root";


    }
}
