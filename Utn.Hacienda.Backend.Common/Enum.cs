using System;

namespace Utn.Hacienda.Backend.Common
{
    public class Enum
    {
        public enum Operation
        {
            Save,
            List,
            Get,
            Delete,
            Login
        }

        public enum Status
        {
            Success,
            Failed
        }

        public enum option_Document
        {
            none,
            delete
        }
        public enum Options_Type_File
        {
            none,
            sale_budget,
            sale_daly,
            result_state,
            sale_delivery,
            KPI,
            inventory1,
            inventory2,
            budget_transfer,
            budget

        }

        public enum Options_Budget_Header
        {
            none,
            generate_Budget,
            update_Budget_Header,
            validate_Budget_Header,
            delete_Budget_Header,
            get_approve
        }

        public enum options_Get_Budget_Header
        {
            none,
            get_Store,
            get_Commercial,
            get_Industry,
            get_All
        }

        public enum options_Store
        {
            none,
            other_Data
        }

        public enum options_Calculation
        {
            none,
            store_Calculation,
            commercial_Calculation
        }

        public enum options_Graphic
        {
            none,
            real_vs_budget,
            comparison_last_year
        }

        public enum option_Inventory_Types
        {
            none,
            general_per_month,
            detail_per_month
        }

        public enum option_Catalog
        {
            none,
            certified_data_exists,
            certified_data_year_exists
        }

        public enum User_Option
        {
            None,
            ListarUsuarioConsulta = 20
        }

        public enum UbicationOption
        {
            none,
            ListUserUbication = 6
        }

        public enum purchase_Order_Options
        {
            None,
            Save_All_Purchase_Order,
            Update_Amounts,
            Update_Purchase_Order_State,
            Send_Purchase_Order,
            Approve_Supervisor_Budget,
            Approve_Superviso_Owner,
            Approve_Superviso_Range,
            Update_Account,
            Cancel,
            Cancel_Payments,
            SEND_EMAIL


        }

        public enum industry_Options
        {
            none,
            ge_Store
        }
        public enum budget_Sale_History_Options
        {
            none,
            get_Sales_History,
            get_Mix_Sales_History,
            get_Personal_History


        }

        public enum budget_Sale_Detail_Options
        {
            none,
            get_sale_detail,
            get_sale_by_personal
        }

        public enum salary_Social_Chargues_Options
        {
            none,
            salary_Account,
            salary_Extra_Ordinary,

            salary_Initial
        }

        public enum option_report
        {
            none,
            result_state
        }

        public enum options_Types_Request
        {
            none,
            save,
            send_Treasury_Department
        }
        public enum options_Configuration_Account_For_Catalog
        {
            summary = 0,
            detail = 1,
            available = 2
        }
    }

}