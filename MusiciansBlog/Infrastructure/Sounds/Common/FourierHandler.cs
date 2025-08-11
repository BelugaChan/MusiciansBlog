using MathNet.Numerics;
using MathNet.Numerics.IntegralTransforms;
using System;

namespace MusiciansBlog.API.Infrastructure.Sounds.Common
{
    public class FourierHandler : IFourierHandler
    {
        public float GetFrequency(byte[] rawData)
        {
            var length = rawData.Length;
            float[] samples = new float[length / 2];
            for (int i = 0; i < samples.Length; i++)
            {
                samples[i] = BitConverter.ToInt16(rawData, i * 2) / 32768f; // Нормализация
            }

            // Применение FFT
            Complex32[] fftBuffer = new Complex32[samples.Length];
            for (int i = 0; i < samples.Length; i++)
            {
                fftBuffer[i] = new Complex32(samples[i], 0); // Заполняем реальную часть
            }
            Fourier.Forward(fftBuffer); // FFT

            // Поиск пиковой частоты
            int peakIndex = 0;
            float maxMagnitude = 0;
            for (int i = 0; i < fftBuffer.Length / 2; i++)
            {
                float magnitude = fftBuffer[i].Magnitude;
                if (magnitude > maxMagnitude)
                {
                    maxMagnitude = magnitude;
                    peakIndex = i;
                }
            }

            // Расчет частоты (Гц)
            float frequency = peakIndex * 44100f / fftBuffer.Length;
            return frequency;
        }
    }
}
