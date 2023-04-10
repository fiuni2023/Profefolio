import styled from 'styled-components';

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
        height: "2em";
        font-family: 'Poppins';
        font-style: normal;
        font-weight: 500;
        font-size: 1em;
        line-height: 2em;
    `;
const Tbody = styled.tbody`
        font-family: 'Poppins';
        font-style: normal;
        font-weight: 300;
        font-size: 1em;
        line-height: 2em;

`;
const TR = styled.tr`
    ${props =>
        props.selected &&
        `
            background: #9C8CBB;
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