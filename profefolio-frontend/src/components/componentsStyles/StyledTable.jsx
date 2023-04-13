import styled from 'styled-components';

const handleSmall = (small, element) => {
    switch (element) {
        case "h-fsize":
            return small ? "0.9em" : "1em";
        case "h-fheiht":
            return small ? "1em" : "2em";
        case "h-align":
            return small ? "center" : "left";    
        case "r-background":
            return small ? "#FFFFFF" : "#9C8CBB";
        case "b-fsize":
            return small ? "0.9em" : "1em"; 
        case "b-fheiht":
            return small ? "1em" : "2em";
        case "b-align":
            return small ? "center" : "left";                 
        default: 
            return "";    
    }
  };


const Table = styled.table`
        width: ${props => props.width};
        box-shadow: 2px 2px 10px 2px rgba(0, 0, 0, 0.1);
        border-radius: 20px;

        tr:first-child th:first-child {
            border-top-left-radius: 20px;
        };
        tr:first-child th:last-child {
            border-top-right-radius: 20px;
        };
        tr:last-child td:first-child {
            border-bottom-left-radius: 20px;
        };
        tr:last-child td:last-child {
            border-bottom-right-radius: 20px;
        };
    `;
const Thead = styled.thead`
        background: ${props => props.background};
        height: 2em;
        font-family: 'Poppins';
        font-style: normal;
        font-weight: 500;
        font-size:  ${({ small }) => handleSmall(small, "h-fsize")} ;
        line-height: ${({ small }) => handleSmall(small, "h-fheiht")};
        text-align: ${({ small }) => handleSmall(small, "h-align")};
    `;
const Tbody = styled.tbody`
        font-family: 'Poppins';
        font-style: normal;
        font-weight: 300;
        font-size:  ${({ small }) => handleSmall(small, "b-fsize")} ;
        line-height:  ${({ small }) => handleSmall(small, "b-fheiht")} ;
        text-align: ${({ small }) => handleSmall(small, "b-align")} ;

`;
const TR = styled.tr`
    background: #FFFFFF;
    ${props =>
        props.selected &&
        `
            background: ${({ small }) => handleSmall(small, "r-background")};
        `}
        ${props => 
        props.clickable &&
        `
            :hover{
                color: #9C8CBB;
                cursor: pointer; 
            }
        `             
        }
    `
    ;
const TH = styled.th`
        padding: 5px 10px;
        text-align: left; 
    `;
const TD = styled.td`
        padding: 6px 10px;
        text-align: left;
        border-top: 1px #C2C2C2 solid;
    `;

export {Table, Thead, Tbody, TR, TH, TD};    