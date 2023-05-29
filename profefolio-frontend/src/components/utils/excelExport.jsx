import React from "react";
import * as FileSaver from 'file-saver';
import XLSX from 'sheetjs-style';
import IconButton from "../IconButton";
import styled from 'styled-components';

const SExport = styled.div`
    display: flex;
    justify-content: flex-end; 
`;

const ExcelExport = ({excelData, fileName}) => {
    const filetype = 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=UTF-8'
    const fileExtension = ".xlsx"
    
    const exportToExcel = async() => {
        const ws = XLSX.utils.json_to_sheet(excelData);
        const wb = { Sheets: { 'data': ws }, SheetNames:['data']};
        const excelBuffer = XLSX.write(wb, {bookType: "xlsx", type: 'array'});
        const data = new Blob([excelBuffer], { type: filetype});
        FileSaver.saveAs(data, fileName, fileExtension );
    }

    return (
        <SExport>
            <IconButton
                buttonType={'download'}
                onClick={() => exportToExcel()}
                enabled={true}
                ></IconButton>
        </SExport> 
    )

}

export default ExcelExport;