package {{PACKAGE_NAME}};

import me.profiluefter.cligui.CLI;
import me.profiluefter.cligui.annotations.CLIApplication;

@CLIApplication("{{PROJECT_NAME}}")
public class Main {
    public static void main(String[] args) {
        CLI.launch(Main.class);
    }
}