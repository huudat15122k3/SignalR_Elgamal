using System.Numerics;
using System.Security.Cryptography;

namespace Server
{
    public class Elgamal
    {
        public (BigInteger, BigInteger, BigInteger) PublicKey { get; private set; }
        public BigInteger PrivateKey { get; private set; }

        public void GenerateKeys(int keySize = 256)
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                BigInteger p = GeneratePrime(keySize, rng);
                BigInteger g = GenerateRandomBigInteger(rng, 2, p - 1);

                BigInteger x = GenerateRandomBigInteger(rng, 1, p - 1);
                BigInteger y = BigInteger.ModPow(g, x, p);

                PublicKey = (p, g, y);
                PrivateKey = x;
            }
        }

        private BigInteger GeneratePrime(int bitLength, RandomNumberGenerator rng)
        {
            while (true)
            {
                byte[] bytes = new byte[bitLength / 8];
                rng.GetBytes(bytes);
                BigInteger p = new BigInteger(bytes);
                if (p.IsProbablyPrime())
                {
                    return p;
                }
            }
        }

        private BigInteger GenerateRandomBigInteger(RandomNumberGenerator rng, BigInteger min, BigInteger max)
        {
            BigInteger result;
            do
            {
                byte[] bytes = max.ToByteArray();
                rng.GetBytes(bytes);
                result = new BigInteger(bytes);
            } while (result < min || result >= max);
            return result;
        }

        public (BigInteger, BigInteger) Encrypt(BigInteger plaintext, (BigInteger, BigInteger, BigInteger) publicKey)
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                BigInteger p = publicKey.Item1;
                BigInteger g = publicKey.Item2;
                BigInteger y = publicKey.Item3;

                BigInteger k = GenerateRandomBigInteger(rng, 1, p - 1);
                BigInteger c1 = BigInteger.ModPow(g, k, p);
                BigInteger c2 = (plaintext * BigInteger.ModPow(y, k, p)) % p;

                return (c1, c2);
            }
        }

        public BigInteger Decrypt((BigInteger, BigInteger) ciphertext, (BigInteger, BigInteger, BigInteger) publicKey, BigInteger privateKey)
        {
            BigInteger p = publicKey.Item1;
            BigInteger c1 = ciphertext.Item1;
            BigInteger c2 = ciphertext.Item2;

            BigInteger s = BigInteger.ModPow(c1, privateKey, p);
            BigInteger sInv = BigInteger.ModPow(s, p - 2, p);  // modular multiplicative inverse using Fermat's little theorem

            BigInteger plaintext = (c2 * sInv) % p;
            return plaintext;
        }
    }

    public static class BigIntegerExtensions
    {
        private static readonly Random Random = new Random();

        public static bool IsProbablyPrime(this BigInteger source, int certainty = 10)
        {
            if (source == 2 || source == 3)
                return true;
            if (source < 2 || source % 2 == 0)
                return false;

            BigInteger d = source - 1;
            int s = 0;

            while (d % 2 == 0)
            {
                d /= 2;
                s += 1;
            }

            for (int i = 0; i < certainty; i++)
            {
                BigInteger a;
                lock (Random)
                {
                    a = 2 + (BigInteger)(Random.NextDouble() * (double)(source - 4));
                }
                BigInteger x = BigInteger.ModPow(a, d, source);

                if (x == 1 || x == source - 1)
                    continue;

                for (int r = 1; r < s; r++)
                {
                    x = BigInteger.ModPow(x, 2, source);
                    if (x == 1)
                        return false;
                    if (x == source - 1)
                        break;
                }

                if (x != source - 1)
                    return false;
            }

            return true;
        }
    }
}
