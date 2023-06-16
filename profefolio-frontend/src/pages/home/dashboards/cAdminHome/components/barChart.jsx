import React from 'react'
import {
  Chart as ChartJS,
  CategoryScale,
  LinearScale,
  BarElement,
  Title,
  Tooltip,
  Legend,
} from 'chart.js';
import { Bar } from 'react-chartjs-2';
//import { SBody, SCard, SCol, SHeader } from '../../../../../components/componentsStyles/StyledDashComponent';

ChartJS.register(
  CategoryScale,
  LinearScale,
  BarElement,
  Title,
  Tooltip,
  Legend
);


const BarChart = ({labels=[], datas=[], datalabel=""}) => {
  const options = {
    maintainAspectRatio: false ,
    responsive: true,
    plugins: {
      legend: {
        position: 'top',
      }
    },
  };

  const data = {
    labels,
    datasets: [
      {
        label: datalabel,
        data: datas,
        backgroundColor: '#31BA8D',
      },
    ]
  };
  return <Bar options={options} data={data} width={100} />;
}

export default BarChart