using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace profefolio.Helpers
{
    public class TimeComparator
    {
        private static string Capitalize(string word)
        {
            return $"{word.ToUpper()[0]}{word.ToLower().Substring(1)}";
        }

        /*
            Retorna los minutos faltantes para llegar al los valores recibidos en las variables day y hora, a partir de 
            la fecha y hora que se tenga en actualTime
        */
        public static int MissingMinutes(DateTime actualTime, string day, string hora)
        {
            var dicc = new Dictionary<string, string>(){
                {"Domingo", "Sunday"},
                {"Lunes", "Monday"},
                {"Martes", "Tuesday"},
                {"Miercoles", "Wednesday"},
                {"Miércoles", "Wednesday"},
                {"Jueves", "Thursday"},
                {"Viernes", "Friday"},
                {"Sabado", "Saturday"},
                {"Sábado", "Saturday"}
            };

            var dia = "";
            if (dicc.ContainsKey(Capitalize(day)))
            {
                dia = dicc[Capitalize(day)];
            }
            else if (dicc.ContainsValue(Capitalize(day)))
            {
                dia = Capitalize(day);
            }
            else
            {
                throw new Exception("Fatal Error");
            }

            // Obtener el próximo día de la semana especificado a partir de la fecha actual
            DayOfWeek diaSemanaX = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), dia);
            int diasHastaDiaSemanaX = ((int)diaSemanaX - (int)actualTime.DayOfWeek + 7) % 7;

            // Calcular la fecha y hora objetivo a partir del dia y la hora
            DateTime fechaObjetivo = actualTime.AddDays(diasHastaDiaSemanaX).Date; // Ignorar la parte de la hora
            DateTime horaObjetivo = DateTime.Parse(hora);

            // Calcular la diferencia de días entre la fecha actual y la fecha y hora objetivo
            TimeSpan diferencia = fechaObjetivo - actualTime.Date + horaObjetivo.TimeOfDay;

            var minutosFaltastes = diferencia.TotalMinutes;

            return (int)minutosFaltastes;

        }


    }
}