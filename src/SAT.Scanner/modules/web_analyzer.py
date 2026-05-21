import requests

class WebSecurityAuditor:
    def __init__(self, target):
        self.target = target if target.startswith('http') else f'http://{target}'
        self.results = {}

    def check_headers(self):
        """Analisi degli header di sicurezza HTTP."""
        important_headers = [
            'Content-Security-Policy',
            'Strict-Transport-Security',
            'X-Frame-Options',
            'X-Content-Type-Options'
        ]
        
        try:
            r = requests.head(self.target, timeout=5, allow_redirects=True)
            header_results = {}
            for h in important_headers:
                header_results[h] = r.headers.get(h, "MISSING")
            return header_results
        except Exception as e:
            return {"error": str(e)}

    def run_all_tests(self):
        self.results['http_headers'] = self.check_headers()
        # Qui si possono aggiungere altri test (es. cookie, ssl, ecc.)
        return self.results
